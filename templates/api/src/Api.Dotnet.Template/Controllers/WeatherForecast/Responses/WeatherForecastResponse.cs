using System;

namespace Api.Dotnet.Template.Controllers.WeatherForecast.Responses
{
    public class WeatherForecastResponse
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
