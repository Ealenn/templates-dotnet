using Api.Dotnet.Template.IntegrationTests.Fake;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Api.Dotnet.Template.IntegrationTests
{
    public class MetricsTests : IClassFixture<FakeApplicationFactory<FakeStartup>>
    {
        private readonly FakeApplicationFactory<FakeStartup> _factory;

        public MetricsTests(FakeApplicationFactory<FakeStartup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task MetricsJson()
        {
            // A
            var httpClient = _factory.CreateClient();

            // A
            var httpResponseMessage = await httpClient.GetAsync("/metrics").ConfigureAwait(false);

            // A
            Assert.NotNull(httpResponseMessage);
            Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
            Assert.Equal("text/plain", httpResponseMessage.Content.Headers.ContentType.MediaType);
        }
    }
}
