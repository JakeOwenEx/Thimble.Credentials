using System.Threading.Tasks;
using Alba;
using Thimble.Credentials.Controllers.Models.CreatePassword;
using Xunit;

namespace Credentials.IntegrationTests.PasswordController.SetPassword
{
    public class WhenInvalidPasswordRequestBodyProvided : ApiTestBase
    {
        public WhenInvalidPasswordRequestBodyProvided(ApiFixture app) : base(app)
        {
        }

        [Theory]
        [InlineData("")]
        [InlineData("423423")]
        [InlineData(".....@")]
        [InlineData("test.test")]
        [InlineData(".....")]
        [InlineData("oneword")]
        public async Task When_Invalid_Email_Provided(string email)
        {
            await System.Scenario(_ =>
            {
                _.Body.JsonInputIs(new PasswordRequest
                {
                    Email = email,
                    Password = "dummypassword"
                });

                _.Post.Url("/useraccount/setPassword");

                _.StatusCodeShouldBe(400);
            });
        }
    }
}