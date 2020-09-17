using System.Threading.Tasks;
using Alba;
using Thimble.Credentials.Controllers.Models.Login;
using Xunit;

namespace Credentials.IntegrationTests.PasswordController.Login
{
    public class WhenValidLoginRequestProvided : ApiTestBase
    {
        public WhenValidLoginRequestProvided(ApiFixture app) : base(app)
        {
        }

        [Fact]
        public async Task When_Login_Is_Successful()
        {
            await System.Scenario(_ =>
            {
                _.Body.JsonInputIs(new LoginRequest
                {
                    UserId = "536c8d81-83c2-4923-b371-7fab524eeb8d",
                    Email = "test-user@emailsim.io",
                    Password = "dummypassword"
                });

                _.Post.Url("/useraccount/login");

                _.StatusCodeShouldBe(200);
            });
        }
    }
}