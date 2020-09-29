using Api.Dotnet.Template.IntegrationTests.Fake;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Api.Dotnet.Template.IntegrationTests
{
    public class HealthCheckTests : IClassFixture<FakeApplicationFactory<FakeStartup>>
    {
        private readonly FakeApplicationFactory<FakeStartup> _fakeApplicationFactory;

        public HealthCheckTests(FakeApplicationFactory<FakeStartup> fakeApplicationFactory)
        {
            _fakeApplicationFactory = fakeApplicationFactory;
        }

        [Fact]
        public async Task LiveProbe_AlwaysReturns200()
        {
            // A
            var httpClient = _fakeApplicationFactory.CreateClient();

            // A
            var httpResponseMessage = await httpClient.GetAsync("/health/live").ConfigureAwait(false);

            // A
            Assert.NotNull(httpResponseMessage);
            Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task ReadyProbe_AlwaysReturns200()
        {
            // A
            var httpClient = _fakeApplicationFactory.CreateClient();

            // A
            var httpResponseMessage = await httpClient.GetAsync("/health/ready").ConfigureAwait(false);

            // A
            Assert.NotNull(httpResponseMessage);
            Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
            Assert.Equal("Degraded", httpResponseMessage.Content.ReadAsStringAsync().Result);
        }
    }
}
