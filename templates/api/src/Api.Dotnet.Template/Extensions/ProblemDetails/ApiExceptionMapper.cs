using Api.Dotnet.Template.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Opw.HttpExceptions.AspNetCore;
using Opw.HttpExceptions.AspNetCore.Mappers;
using System;

namespace Api.Dotnet.Template.Extensions.ProblemDetails
{
    internal sealed class ApiExceptionMapper : ProblemDetailsExceptionMapper<ApiException>
    {
        public ApiExceptionMapper(IOptions<HttpExceptionsOptions> options) : base(options)
        {
        }

        public override IStatusCodeActionResult Map(Exception exception, HttpContext context)
        {
            if (!(exception is ApiException ex))
                throw new ArgumentOutOfRangeException(nameof(exception), exception, "Exception is not of type ApiException.");

            var customError = new ApiProblemDetails
            {
                Status = MapStatus(ex, context),
                Type = MapType(ex, context),
                Title = MapTitle(ex, context),
                Detail = MapDetail(ex, context),
                Instance = MapInstance(ex, context),
                Code = ex.ErrorCode,
                Message = MapDetail(ex, context)
            };

            return new ApiErrorResult(customError);
        }

        internal class ApiErrorResult : ProblemDetailsResult
        {
            public ApiErrorResult(ApiProblemDetails apiProblemDetails) : base(apiProblemDetails)
            {
                StatusCode = apiProblemDetails.Status;
                DeclaredType = apiProblemDetails.GetType();

                ContentTypes.Add(new MediaTypeHeaderValue("application/problem+json"));
                ContentTypes.Add(new MediaTypeHeaderValue("application/problem+xml"));
            }
        }
    }
}
