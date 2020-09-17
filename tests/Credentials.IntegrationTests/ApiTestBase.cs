using Alba;
using Xunit;

namespace Credentials.IntegrationTests
{
    public abstract class ApiTestBase : IClassFixture<ApiFixture>
    {
        protected readonly SystemUnderTest System;

        protected ApiTestBase(ApiFixture app)
        {
            System = app.SystemUnderTest;
        }
    }
}