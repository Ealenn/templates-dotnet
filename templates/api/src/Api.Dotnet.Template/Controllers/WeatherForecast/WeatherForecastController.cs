using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Api.Dotnet.Template.Extensions.ProblemDetails;
using Api.Dotnet.Template.Controllers.WeatherForecast.Responses;
using Api.Dotnet.Template.Controllers.WeatherForecast.Requests;
using Api.Dotnet.Template.Controllers.WeatherForecast.Exceptions;

namespace Api.Dotnet.Template.Controllers.WeatherForecast
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMapper _mapper;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Weather Forecast",
            Description = "Get Weather Forecast with X-Temperature header"
        )]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherForecastResponse))]
        [SwaggerResponseHeader(200, "X-Temperature", "string", "Temperature")]
        public IActionResult Get()
        {
            var temperature = new Random().Next(-20, 55);
            _logger.LogTrace($"[GET] {nameof(WeatherForecastController)} with {temperature}C");

            Request.HttpContext.Response.Headers.Add("X-Temperature", temperature.ToString(CultureInfo.InvariantCulture));
            return Ok(new WeatherForecastResponse
            {
                Date = DateTime.Now,
                TemperatureC = temperature,
                Summary = "Freezing"
            });
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Weather Forecast By Id",
            Description = "Throw Weather Forecast Not Found Exception"
        )]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherForecastResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiProblemDetails))]
        public IActionResult Get(string id)
        {
            throw new WeatherForecastNotFoundException(id);
        }

        [HttpGet("header")]
        [SwaggerOperation(
            Summary = "Weather Forecast With Required Header",
            Description = "Return X-Header value"
        )]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public IActionResult GetWithHeader([FromHeader(Name = "X-Header")][Required] string requiredHeader)
        {
            return Ok(requiredHeader);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Insert Weather Forecast",
            Description = "Add Weather Forecast"
        )]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherForecastResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public IActionResult Post([FromBody] AddWeatherForecastRequest weatherForecastRequest)
        {
            return Ok(_mapper.Map<WeatherForecastResponse>(weatherForecastRequest));
        }
    }
}
