using System.Threading.Tasks;
using Alba;
using Thimble.Credentials.Controllers.Models.CreatePassword;
using Xunit;

namespace Credentials.IntegrationTests.PasswordController.DeleteUser
{
    public class WhenValidUserIdProvided : ApiTestBase
    {
        public WhenValidUserIdProvided(ApiFixture app) : base(app)
        {
        }

        [Fact]
        private async Task Will_Delete_User_Successfully()
        {
            var userId = await Setup();
            
            await System.Scenario(_ =>
            {
                _.Delete.Url($"/useraccount/{userId}/delete");

                _.StatusCodeShouldBe(204);
            });
        }

        private async Task<string> Setup()
        {
            var response = await System.Scenario(_ =>
            {
                _.Body.JsonInputIs(new PasswordRequest
                {
                    Email = "test-delete-user@emailsim.io",
                    Password = "dummypassword"
                });
                _.Post.Url("/useraccount/setPassword");
                _.StatusCodeShouldBe(201);
            });
            var body = response.ResponseBody.ReadAsJson<PasswordResponse>();
            return body.UserId;
        }
    }
}