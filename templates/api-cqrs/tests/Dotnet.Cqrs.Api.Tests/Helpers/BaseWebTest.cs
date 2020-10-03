using Dotnet.Cqrs.Api.Tests.Fake;
using Xunit;
using Xunit.Abstractions;

namespace Dotnet.Cqrs.Api.Tests.Helpers
{
    public abstract class BaseWebTest : IClassFixture<FakeApplicationFactory<FakeStartup>>
    {
        public FakeApplicationFactory<FakeStartup> ApplicationFactory { get; }

        protected BaseWebTest(FakeApplicationFactory<FakeStartup> applicationFactory, ITestOutputHelper outputHelper)
        {
            ApplicationFactory = applicationFactory;
            ApplicationFactory.Output = outputHelper;
        }
    }
}
