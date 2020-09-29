using Api.Dotnet.Template.IntegrationTests.Fake;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Api.Dotnet.Template.IntegrationTests
{
    public class SwaggerTests : IClassFixture<FakeApplicationFactory<FakeStartup>>
    {
        private readonly FakeApplicationFactory<FakeStartup> _factory;

        public SwaggerTests(FakeApplicationFactory<FakeStartup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task SwaggerJson()
        {
            // A
            var httpClient = _factory.CreateClient();

            // A
            var httpResponseMessage = await httpClient.GetAsync("/swagger/v1/swagger.json").ConfigureAwait(false);

            // A
            Assert.NotNull(httpResponseMessage);
            Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
            Assert.Equal("application/json", httpResponseMessage.Content.Headers.ContentType.MediaType);
        }

        [Fact]
        public async Task SwaggerUi()
        {
            // A
            var httpClient = _factory.CreateClient();

            // A
            var httpResponseMessage = await httpClient.GetAsync("/swagger").ConfigureAwait(false);

            // A
            Assert.NotNull(httpResponseMessage);
            Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
            Assert.Equal("text/html", httpResponseMessage.Content.Headers.ContentType.MediaType);
        }
    }
}
