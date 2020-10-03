using Dotnet.Cqrs.Api.Tests.Fake;
using Dotnet.Cqrs.Api.Tests.Helpers;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Dotnet.Cqrs.Api.Tests.Controllers
{
    public class SwaggerTests : BaseWebTest
    {
        public SwaggerTests(FakeApplicationFactory<FakeStartup> applicationFactory, ITestOutputHelper outputHelper)
            : base(applicationFactory, outputHelper)
        {
        }

        [Fact]
        public async Task TestSwaggerJson()
        {
            // A
            var httpClient = ApplicationFactory.CreateClient();

            // A
            var httpResponseMessage = await httpClient.GetAsync("/swagger/v1/swagger.json").ConfigureAwait(false);

            // A
            Assert.NotNull(httpResponseMessage);
            Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
            Assert.Equal("application/json", httpResponseMessage.Content.Headers.ContentType.MediaType);
        }

        [Fact]
        public async Task TestSwagger()
        {
            // A
            var httpClient = ApplicationFactory.CreateClient();

            // A
            var httpResponseMessage = await httpClient.GetAsync("/swagger").ConfigureAwait(false);

            // A
            Assert.NotNull(httpResponseMessage);
            Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
            Assert.Equal("text/html", httpResponseMessage.Content.Headers.ContentType.MediaType);
        }

        [Fact]
        public async Task TestRedoc()
        {
            // A
            var httpClient = ApplicationFactory.CreateClient();

            // A
            var httpResponseMessage = await httpClient.GetAsync("/").ConfigureAwait(false);

            // A
            Assert.NotNull(httpResponseMessage);
            Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
            Assert.Equal("text/html", httpResponseMessage.Content.Headers.ContentType.MediaType);
        }
    }
}
