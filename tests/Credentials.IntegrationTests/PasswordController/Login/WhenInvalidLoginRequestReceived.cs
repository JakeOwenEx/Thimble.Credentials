using System.Threading.Tasks;
using Alba;
using Thimble.Credentials.Controllers.Models.Login;
using Xunit;

namespace Credentials.IntegrationTests.PasswordController.Login
{
    public class WhenInvalidLoginRequestReceived : ApiTestBase
    {
        public WhenInvalidLoginRequestReceived(ApiFixture app) : base(app)
        {
        }
        
        // when invalid/wrong password given

        [Fact]
        public async Task When_Email_Not_Provided()
        {
            await System.Scenario(_ =>
            {
                _.Body.JsonInputIs(new LoginRequest
                {
                    UserId = "fakeuserid",
                    Email = "",
                    Password = "dummypassword"
                });

                _.Post.Url("/useraccount/login");

                _.StatusCodeShouldBe(400);
            });
        }

        [Theory]
        [InlineData("423423")]
        [InlineData(".....@")]
        [InlineData("test.test")]
        [InlineData(".....")]
        [InlineData("oneword")]
        public async Task When_Invalid_Email_Provided(string email)
        {
            await System.Scenario(_ =>
            {
                _.Body.JsonInputIs(new LoginRequest
                {
                    UserId = "fakeuserid",
                    Email = email,
                    Password = "dummypassword"
                });

                _.Post.Url("/useraccount/login");

                _.StatusCodeShouldBe(401);
            });
        }
        
        [Fact]
        public async Task When_Wrong_Password_Provided()
        {
            await System.Scenario(_ =>
            {
                _.Body.JsonInputIs(new LoginRequest
                {
                    UserId = "536c8d81-83c2-4923-b371-7fab524eeb8d",
                    Email = "test-user@emailsim.io",
                    Password = "wrong-passsword"
                });

                _.Post.Url("/useraccount/login");

                _.StatusCodeShouldBe(401);
            });
        }


        [Fact]
        public async Task When_User_Does_Not_Exist()
        {
            await System.Scenario(_ =>
            {
                _.Body.JsonInputIs(new LoginRequest
                {
                    UserId = "a-fake-user-id",
                    Email = "gibberish@emailsim.io",
                    Password = "dummypassword"
                });

                _.Post.Url("/useraccount/login");

                _.StatusCodeShouldBe(401);
            });
        }
    }
}