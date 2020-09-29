using Api.Dotnet.Template.Exceptions;
using System.Net;

namespace Api.Dotnet.Template.Controllers.WeatherForecast.Exceptions
{
    public sealed class WeatherForecastNotFoundException : ApiException
    {
        public WeatherForecastNotFoundException(string weatherForecastId) : base(HttpStatusCode.NotFound, "WeatherForecastNotFound", $"Weather Forecast {weatherForecastId} not found")
        {
        }

        public override string HelpLink { get; set; }
    }
}
