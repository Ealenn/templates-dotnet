using Api.Dotnet.Template.Extensions.Options;
using Dawn;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Opw.HttpExceptions.AspNetCore;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Api.Dotnet.Template.IntegrationTests")]
[assembly: InternalsVisibleTo("Api.Dotnet.Template.UnitTests")]
namespace Api.Dotnet.Template.Extensions
{
    internal static class AppBuilderExtensions
    {
        internal static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder applicationBuilder, bool @override = true)
        {
            Guard.Argument(applicationBuilder, nameof(applicationBuilder)).NotNull();
            applicationBuilder.UseHttpExceptions();
            return applicationBuilder;
        }

        internal static IApplicationBuilder UseOpenApi(this IApplicationBuilder applicationBuilder)
        {
            Guard.Argument(applicationBuilder, nameof(applicationBuilder)).NotNull();
            var options = applicationBuilder.ApplicationServices.GetRequiredService<IOptions<OpenApiOptions>>();

            applicationBuilder.UseSwagger();

            if (options.Value.EnableSwaggerUi)
            {
                applicationBuilder.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/{options.Value.Version}/swagger.json", options.Value.Title));
            }

            if (options.Value.EnableRedoc)
            {
                applicationBuilder.UseReDoc(c =>
                {
                    c.RoutePrefix = string.Empty;
                    c.ConfigObject.ExpandResponses = "200,201";
                    c.ConfigObject.RequiredPropsFirst = true;
                    c.DocumentTitle = options.Value.Title;
                    c.SpecUrl = $"/swagger/{options.Value.Version}/swagger.json";
                });
            }

            return applicationBuilder;
        }
    }
}
