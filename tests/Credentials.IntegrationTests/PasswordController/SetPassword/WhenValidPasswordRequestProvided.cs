using System.Threading.Tasks;
using Alba;
using Thimble.Credentials.Controllers.Models.CreatePassword;
using Xunit;

namespace Credentials.IntegrationTests.PasswordController.SetPassword
{
    public class WhenValidPasswordRequestProvided : ApiTestBase
    {
        public WhenValidPasswordRequestProvided(ApiFixture app) : base(app)
        {
        }
        
        [Fact]
        public async Task Should_return_created_status()
        {
            var response = await System.Scenario(_ =>
            {
                _.Body.JsonInputIs(new PasswordRequest
                {
                    Email = "test-user@emailsim.io",
                    Password = "dummypassword"
                });

                _.Post.Url("/useraccount/setPassword");

                _.StatusCodeShouldBe(201);
            });

            var body = response.ResponseBody.ReadAsJson<PasswordResponse>();
            await TearDown(body.UserId);
        }

        private async Task TearDown(string userId)
        {
            await System.Scenario(_ =>
            {
                _.Delete.Url($"/useraccount/{userId}/delete");
                _.StatusCodeShouldBe(204);
            });
        }

    }
}