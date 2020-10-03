using Microsoft.AspNetCore.Builder;
using Dawn;
using Hellang.Middleware.ProblemDetails;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Dotnet.Cqrs.Api.Options;

namespace Dotnet.Cqrs.Api.Extensions
{
    internal static class AppBuilderExtensions
    {
        internal static IApplicationBuilder UseAppExceptionHandler(this IApplicationBuilder applicationBuilder)
        {
            Guard.Argument(applicationBuilder, nameof(applicationBuilder)).NotNull();

            applicationBuilder.UseProblemDetails();

            return applicationBuilder;
        }

        internal static IApplicationBuilder UseAppOpenApi(this IApplicationBuilder applicationBuilder)
        {
            Guard.Argument(applicationBuilder, nameof(applicationBuilder)).NotNull();

            var options = applicationBuilder.ApplicationServices.GetRequiredService<IOptions<OpenApiOptions>>();

            applicationBuilder.UseSwagger();
            applicationBuilder.UseSwaggerUI(c => {
                c.SwaggerEndpoint($"/swagger/{options.Value.Version}/swagger.json", options.Value.Title);
            });

            applicationBuilder.UseReDoc(c =>
            {
                c.RoutePrefix = string.Empty;
                c.ConfigObject.ExpandResponses = "200,201";
                c.ConfigObject.RequiredPropsFirst = true;
                c.DocumentTitle = options.Value.Title;
                c.SpecUrl = $"/swagger/{options.Value.Version}/swagger.json";
            });

            return applicationBuilder;
        }
    }
}
