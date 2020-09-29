using AutoMapper;
using Api.Dotnet.Template.Controllers.WeatherForecast.Requests;
using Api.Dotnet.Template.Controllers.WeatherForecast.Responses;

namespace Api.Dotnet.Template.Mappers.WeatherForecast
{
    public class WeatherForecastProfile : Profile
    {
        public WeatherForecastProfile()
        {
            CreateMap<AddWeatherForecastRequest, WeatherForecastResponse>();
        }
    }
}
