using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace AspNetCoreWebApi.Integration.Tests.Infrastructure
{
    public abstract class BaseTest : IClassFixture<ApplicationFactoryTest<StartupTest>>
    {
        protected WebApplicationFactory<StartupTest> Factory { get; }

        public BaseTest(ApplicationFactoryTest<StartupTest> factory)
        {
            Factory = factory;            
        }

        // Add you other helper methods here
    }
}