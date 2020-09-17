using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Thimble.Credentials.AWS.Dynamo.Credentials;
using Thimble.Credentials.Controllers.Authorization;
using Thimble.Credentials.Controllers.Models.CreatePassword;
using Thimble.Credentials.Controllers.Models.Login;
using Thimble.Credentials.Controllers.TraceId;
using Thimble.Credentials.logging;

namespace Thimble.Credentials.Controllers
{
   [Route("credentials/")]
   [ApiController]
   [Authorization]
   [TraceIdResolver]
   public class CredentialsController : ControllerBase
   {
      private readonly ICredentialsDynamoClient _credentialsDynamoClient;
      private readonly IThimbleLogger _thimbleLogger;

      public CredentialsController(
         ICredentialsDynamoClient credentialsDynamoClient,
         IThimbleLogger thimbleLogger)
      {
         _credentialsDynamoClient = credentialsDynamoClient;
         _thimbleLogger = thimbleLogger;
      }

      [HttpPost("createPassword")]
      public async Task<ActionResult> CreatePassword([FromBody] PasswordRequest passwordRequest)
      {
         _thimbleLogger.Log(passwordRequest.UserId, "credentials.registration.started");
         await _credentialsDynamoClient.CreatePassword(passwordRequest);
         _thimbleLogger.Log(passwordRequest.UserId, "credentials.registration.successful");
         return Created("Thimble.Password", new PasswordResponse{UserId = passwordRequest.UserId});
      }

      [HttpPost("login")]
      public async Task<LoginResponse> Login([FromBody] LoginRequest passwordRequest)
      {
         _thimbleLogger.Log("credentials.login.started");
         var verified = await _credentialsDynamoClient.Login(passwordRequest);
         _thimbleLogger.Log("credentials.login.successful");
         return new LoginResponse{Verified = verified};
      }
      
      [HttpDelete("{userId}/delete")]
      public async Task<ActionResult> DeleteUser(string userId)
      {
         _thimbleLogger.Log(userId, "credentials.delete.started");
         await _credentialsDynamoClient.DeleteUser(userId);
         _thimbleLogger.Log(userId, "credentials.delete.successful");
         return NoContent();
      }   
   }
    
}