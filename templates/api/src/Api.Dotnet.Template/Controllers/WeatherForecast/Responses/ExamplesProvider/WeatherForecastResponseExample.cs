using Swashbuckle.AspNetCore.Filters;
using System;

namespace Api.Dotnet.Template.Controllers.WeatherForecast.Responses.ExamplesProvider
{
    public class WeatherForecastResponseExample : IExamplesProvider<WeatherForecastResponse>
    {
        public WeatherForecastResponse GetExamples()
        {
            return new WeatherForecastResponse
            {
                Date = DateTime.Now,
                TemperatureC = 37,
                Summary = "Hot"
            };
        }
    }
}