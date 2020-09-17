using System;
using Alba;
using Thimble.Credentials;

namespace Credentials.IntegrationTests
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ApiFixture : IDisposable
    {
        public readonly SystemUnderTest SystemUnderTest;
        
        public ApiFixture()
        {
            var hostBuilder = Program.CreateHostBuilder(new string[0]);
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "tests");
            SystemUnderTest = new SystemUnderTest(hostBuilder);
        }
        
        public void Dispose()
        {
            SystemUnderTest?.Dispose();
        }
    }
}