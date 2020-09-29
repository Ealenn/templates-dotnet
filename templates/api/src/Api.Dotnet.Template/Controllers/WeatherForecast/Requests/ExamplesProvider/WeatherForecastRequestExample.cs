using Swashbuckle.AspNetCore.Filters;
using System;

namespace Api.Dotnet.Template.Controllers.WeatherForecast.Requests.ExamplesProvider
{
    public class WeatherForecastRequestExample : IExamplesProvider<AddWeatherForecastRequest>
    {
        public AddWeatherForecastRequest GetExamples()
        {
            return new AddWeatherForecastRequest
            {
                Date = DateTime.Now,
                TemperatureC = 12,
                Summary = "Hot",
                Link = "https://example.com",
                Contact = "example@host.com"
            };
        }
    }
}
