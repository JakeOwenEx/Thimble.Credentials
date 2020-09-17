using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;
using Thimble.Credentials.Controllers.Models.CreatePassword;
using Thimble.Credentials.Controllers.Models.Login;

namespace Thimble.Credentials.AWS.Dynamo.Credentials
{
    public interface ICredentialsDynamoClient
    {
        Task CreatePassword(PasswordRequest passwordRequest);
        Task<bool> Login(LoginRequest loginRequest);
        Task<DeleteItemResponse> DeleteUser(string userId);
    }
}