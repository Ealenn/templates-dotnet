using Dotnet.Cqrs.Api.Tests.Fake;
using Dotnet.Cqrs.Api.Tests.Helpers;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Dotnet.Cqrs.Api.Tests.Controllers
{
    public class MetricsTests : BaseWebTest
    {
        public MetricsTests(FakeApplicationFactory<FakeStartup> applicationFactory, ITestOutputHelper outputHelper)
            : base(applicationFactory, outputHelper)
        {
        }

        [Fact]
        public async Task MetricsJson()
        {
            // A
            var httpClient = ApplicationFactory.CreateClient();

            // A
            var httpResponseMessage = await httpClient.GetAsync("/metrics").ConfigureAwait(false);

            // A
            Assert.NotNull(httpResponseMessage);
            Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
            Assert.Equal("text/plain", httpResponseMessage.Content.Headers.ContentType.MediaType);
        }
    }
}
