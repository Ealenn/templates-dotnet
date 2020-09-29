using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace Api.Dotnet.Template.Controllers.WeatherForecast.Requests
{
    public class AddWeatherForecastRequest
    {
        [Required]
        public DateTime? Date { get; set; }

        [Required, Range(-30, 60)]
        public int TemperatureC { get; set; }

        public string Summary { get; set; }

        [Url]
        public string Link { get; set; }

        [EmailAddress]
        public string Contact { get; set; }
    }
}
