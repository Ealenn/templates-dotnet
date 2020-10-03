using Dotnet.Cqrs.Api.Tests.Fake;
using Dotnet.Cqrs.Api.Tests.Helpers;
using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Dotnet.Cqrs.Api.Tests.Controllers
{
    public class HealthCheckTests : BaseWebTest
    {
        public HealthCheckTests(FakeApplicationFactory<FakeStartup> applicationFactory, ITestOutputHelper outputHelper)
            : base(applicationFactory, outputHelper)
        {
        }

        [Fact]
        public async Task GetLiveAsync()
        {
            // Arrange
            var client = ApplicationFactory.CreateClient();

            // Act
            var response = await client.GetAsync("/health/live");

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetReadyAsync()
        {
            // Arrange
            var client = ApplicationFactory.CreateClient();

            // Act
            var response = await client.GetAsync("/health/ready");

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.ServiceUnavailable);
        }
    }
}
