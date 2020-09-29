using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using Api.Dotnet.Template.IntegrationTests.Fake;
using Xunit;
using Api.Dotnet.Template.Controllers.WeatherForecast.Responses;

namespace Api.Dotnet.Template.IntegrationTests.Controllers
{
    public class WeatherForecastControllerTests : IClassFixture<FakeApplicationFactory<FakeStartup>>
    {
        private readonly FakeApplicationFactory<FakeStartup> _fakeApplicationFactory;

        public WeatherForecastControllerTests(FakeApplicationFactory<FakeStartup> fakeApplicationFactory)
        {
            _fakeApplicationFactory = fakeApplicationFactory;
        }

        [Fact]
        public async Task Get_ReturnSuccessAndCorrectValue()
        {
            // Arrange
            var client = _fakeApplicationFactory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/WeatherForecast").ConfigureAwait(false);

            // Assert
            response.EnsureSuccessStatusCode();
            response.Content.Headers.ContentType
                .Should().Equals("application/json; charset=utf-8");

            var jsonResponse = JsonConvert.DeserializeObject<WeatherForecastResponse>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            jsonResponse.Should().NotBeNull();
            jsonResponse.Should().BeOfType<WeatherForecastResponse>();
            jsonResponse.Should().Match<WeatherForecastResponse>(obj => obj.Summary.Equals("Freezing"));
        }

        [Fact]
        public async Task GetById_Return404NotFount()
        {
            // Arrange
            var client = _fakeApplicationFactory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/WeatherForecast/1").ConfigureAwait(false);

            // Assert
            response.Content.Headers.ContentType.Should().Equals("application/json; charset=utf-8");
            response.StatusCode.Should().Equals(HttpStatusCode.NotFound);
        }
    }
}
