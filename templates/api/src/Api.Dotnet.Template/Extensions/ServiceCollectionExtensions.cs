using Api.Dotnet.Template.Exceptions;
using Api.Dotnet.Template.Extensions.Options;
using Api.Dotnet.Template.Extensions.ProblemDetails;
using Dawn;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Opw.HttpExceptions.AspNetCore;
using Opw.HttpExceptions.AspNetCore.Mappers;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Api.Dotnet.Template.IntegrationTests")]
[assembly: InternalsVisibleTo("Api.Dotnet.Template.UnitTests")]
namespace Api.Dotnet.Template.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddMvc(this IServiceCollection services, IWebHostEnvironment env)
        {
            Guard.Argument(services, nameof(services)).NotNull();
            Guard.Argument(env, nameof(env)).NotNull();

            services.AddControllers()
                .AddNewtonsoftJson()
                .AddHttpExceptions(o => ConfigureProblemDetails(o, env));

            return services;
        }

        private static void ConfigureProblemDetails(HttpExceptionsOptions options, IHostEnvironment environment)
        {
            Guard.Argument(environment, nameof(environment)).NotNull();

            options.IncludeExceptionDetails = _ => environment.IsDevelopment();
            options.ExceptionMapper<ApiException, ApiExceptionMapper>();
            options.ExceptionMapper<Exception, ProblemDetailsExceptionMapper<Exception>>();
        }

        internal static IServiceCollection AddHeaderForward(this IServiceCollection services, IWebHostEnvironment env)
        {
            Guard.Argument(services, nameof(services)).NotNull();
            Guard.Argument(env, nameof(env)).NotNull();

            if (!env.IsDevelopment())
            {
                services.Configure<ForwardedHeadersOptions>(options => options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto);
            }

            return services;
        }

        internal static IServiceCollection AddCors(this IServiceCollection services, IConfiguration config)
        {
            Guard.Argument(services, nameof(services)).NotNull();
            Guard.Argument(config, nameof(config)).NotNull();

            services.Configure<CorsOptions>(config.GetSection(CorsOptions.OptionSection));

            services.AddCors(c =>
            {
                var options = config.GetSection(CorsOptions.OptionSection).Get<CorsOptions>();

                if (options != null)
                {
                    c.AddDefaultPolicy(policy =>
                        policy.WithOrigins(options.Origins)
                            .WithHeaders(options.Headers)
                            .WithMethods(options.Methods));
                }
                else
                {
                    c.AddDefaultPolicy(
                        policy => policy
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .SetIsOriginAllowedToAllowWildcardSubdomains());
                }
            });

            return services;
        }

        internal static IServiceCollection AddOpenApi(this IServiceCollection services, IConfiguration config)
        {
            Guard.Argument(services, nameof(services)).NotNull();
            Guard.Argument(config, nameof(config)).NotNull();

            services.Configure<OpenApiOptions>(config.GetSection(OpenApiOptions.OptionSection));
            services
                .AddSwaggerGen(c =>
                {
                    var options = config.GetSection(OpenApiOptions.OptionSection).Get<OpenApiOptions>() ?? new OpenApiOptions();

                    c.SwaggerDoc(options.Version, new OpenApiInfo
                    {
                        Version = options.Version,
                        Title = options.Title,
                        Contact = new OpenApiContact
                        {
                            Email = options.ContactEmail,
                            Name = options.ContactName
                        },
                        Description = options.Description,
                        Extensions = new Dictionary<string, IOpenApiExtension>
                        {
                            {
                                "x-logo",
                                new OpenApiObject
                                {
                                    { "url", new OpenApiString(options.Logo) },
                                    { "altText", new OpenApiString(options.Title) }
                                }
                            }
                        }
                    });
                    c.EnableAnnotations();
                    c.ExampleFilters();
                    c.OperationFilter<AddResponseHeadersFilter>();
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                })
                .AddSwaggerGenNewtonsoftSupport()
                .AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

            return services;
        }
    }
}
