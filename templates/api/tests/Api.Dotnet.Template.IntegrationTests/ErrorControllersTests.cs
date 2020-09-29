using Api.Dotnet.Template.IntegrationTests.Fake;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Api.Dotnet.Template.IntegrationTests
{
    public class ErrorControllersTests : IClassFixture<FakeApplicationFactory<FakeStartup>>
    {
        private readonly FakeApplicationFactory<FakeStartup> _fakeApplicationFactory;

        public ErrorControllersTests(FakeApplicationFactory<FakeStartup> fakeApplicationFactory)
        {
            _fakeApplicationFactory = fakeApplicationFactory;
        }

        [Fact]
        public async Task GetBadRequest_AlwaysReturns400InSpecificFormat()
        {
            // A
            var httpClient = _fakeApplicationFactory.CreateClient();

            // A
            var httpResponseMessage = await httpClient.GetAsync("/api/errors/400").ConfigureAwait(false);

            // A
            Assert.NotNull(httpResponseMessage);
            Assert.Equal(HttpStatusCode.BadRequest, httpResponseMessage.StatusCode);
            var content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.NotNull(content);
            var problem = JsonConvert.DeserializeObject<Microsoft.AspNetCore.Mvc.ProblemDetails>(content);
            Assert.NotNull(problem);
            Assert.NotNull(problem.Status);
            Assert.Equal((int)HttpStatusCode.BadRequest, problem.Status.Value);
        }

        [Fact]
        public async Task GetUnauthorized_AlwaysReturns401InSpecificFormat()
        {
            // A
            var httpClient = _fakeApplicationFactory.CreateClient();

            // A
            var httpResponseMessage = await httpClient.GetAsync("/api/errors/401").ConfigureAwait(false);

            // A
            Assert.NotNull(httpResponseMessage);
            Assert.Equal(HttpStatusCode.Unauthorized, httpResponseMessage.StatusCode);
            var content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.NotNull(content);
            var problem = JsonConvert.DeserializeObject<Microsoft.AspNetCore.Mvc.ProblemDetails>(content);
            Assert.NotNull(problem);
            Assert.NotNull(problem.Status);
            Assert.Equal((int)HttpStatusCode.Unauthorized, problem.Status.Value);
        }

        [Fact]
        public async Task GetNotFound_AlwaysReturns404InSpecificFormat()
        {
            // A
            var httpClient = _fakeApplicationFactory.CreateClient();

            // A
            var httpResponseMessage = await httpClient.GetAsync("/api/errors/404").ConfigureAwait(false);

            // A
            Assert.NotNull(httpResponseMessage);
            Assert.Equal(HttpStatusCode.NotFound, httpResponseMessage.StatusCode);
            var content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.NotNull(content);
            var problem = JsonConvert.DeserializeObject<Microsoft.AspNetCore.Mvc.ProblemDetails>(content);
            Assert.NotNull(problem);
            Assert.NotNull(problem.Status);
            Assert.Equal((int)HttpStatusCode.NotFound, problem.Status.Value);
        }

        [Fact]
        public async Task GetInternalServerError_AlwaysReturns500InSpecificFormat()
        {
            // A
            var httpClient = _fakeApplicationFactory.CreateClient();

            // A
            var httpResponseMessage = await httpClient.GetAsync("/api/errors/500").ConfigureAwait(false);

            // A
            Assert.NotNull(httpResponseMessage);
            Assert.Equal(HttpStatusCode.InternalServerError, httpResponseMessage.StatusCode);
            var content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.NotNull(content);
            var problem = JsonConvert.DeserializeObject<Microsoft.AspNetCore.Mvc.ProblemDetails>(content);
            Assert.NotNull(problem);
            Assert.NotNull(problem.Status);
            Assert.Equal((int)HttpStatusCode.InternalServerError, problem.Status.Value);
        }
    }
}
