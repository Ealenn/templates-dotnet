using AutoMapper;
using Dotnet.Cqrs.Api.Controllers.Example.Models.Queries;
using Dotnet.Cqrs.Api.Controllers.Example.Models.Responses;
using Dotnet.Cqrs.Api.Controllers.Shared.Queries;
using Dotnet.Cqrs.Domain.Example;
using Dotnet.Cqrs.Service.Example.Abstractions;
using Dotnet.Cqrs.Service.Example.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace Dotnet.Cqrs.Api.Controllers.Example
{
    [ApiController]
    [Route("Example")]
    [Produces(MediaTypeNames.Application.Json)]
    public class ExampleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IExampleService _exampleService;

        public ExampleController(IMapper mapper, IExampleService exampleService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _exampleService = exampleService ?? throw new ArgumentNullException(nameof(exampleService));
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Retrieve paginated examples",
            Description = "Get examples with pagination"
        )]
        [ProducesResponseType(typeof(IEnumerable<GetExampleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<GetExampleResponse>>> GetExample([FromQuery] PaginatedQuery paginatedQuery, CancellationToken cancellationToken)
        {
            var examplesReadModel = await _exampleService.GetAsync(paginatedQuery.Skip, paginatedQuery.Take, cancellationToken);
            return Ok(_mapper.Map<IEnumerable<GetExampleResponse>>(examplesReadModel));
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Retrieve a specific Example",
            Description = "Get Example by identifier"
        )]
        [ProducesResponseType(typeof(GetExampleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetExampleResponse>> GetExample([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var exampleReadModel = await _exampleService.GetByIdAsync(ExampleId.With(id), cancellationToken);
            if (exampleReadModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GetExampleResponse>(exampleReadModel));
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Create Example",
            Description = "Create new Example"
        )]
        [ProducesResponseType(typeof(GetExampleResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetExampleResponse>> CreateExample([FromBody] CreateExampleQuery query, CancellationToken cancellationToken)
        {
            var Example = await _exampleService.CreateAsync(_mapper.Map<CreateExampleModel>(query), cancellationToken);
            var exampleModel = _mapper.Map<GetExampleResponse>(Example);

            return CreatedAtAction(nameof(CreateExample), new { exampleModel.Id }, exampleModel);
        }
    }
}
