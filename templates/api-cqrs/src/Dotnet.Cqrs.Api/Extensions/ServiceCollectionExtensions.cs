using Dawn;
using Dotnet.Cqrs.Api.Options;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dotnet.Cqrs.Api.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddAppMvc(this IServiceCollection services, IWebHostEnvironment env)
        {
            Guard.Argument(services, nameof(services)).NotNull();
            Guard.Argument(env, nameof(env)).NotNull();

            services.AddControllers()
                .AddApplicationPart(typeof(Startup).Assembly)
                .AddNewtonsoftJson();

            services.AddProblemDetails(o => ConfigureProblemDetails(o, env));

            return services;
        }

        internal static IServiceCollection AddAppHeaderForward(this IServiceCollection services, IWebHostEnvironment env)
        {
            Guard.Argument(services, nameof(services)).NotNull();
            Guard.Argument(env, nameof(env)).NotNull();

            if (!env.IsDevelopment())
            {
                services.Configure<ForwardedHeadersOptions>(options => options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto);
            }

            return services;
        }

        internal static IServiceCollection AddAppCors(this IServiceCollection services, IConfiguration config)
        {
            Guard.Argument(services, nameof(services)).NotNull();
            Guard.Argument(config, nameof(config)).NotNull();

            services.Configure<CorsOptions>(config.GetSection(CorsOptions.SectionKey));

            services.AddCors(c =>
            {
                var options = config.GetSection(CorsOptions.SectionKey).Get<CorsOptions>();

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

        internal static IServiceCollection AddAppOpenApi(this IServiceCollection services, IConfiguration config)
        {
            Guard.Argument(services, nameof(services)).NotNull();
            Guard.Argument(config, nameof(config)).NotNull();

            services.Configure<OpenApiOptions>(config.GetSection(OpenApiOptions.SectionKey));
            services
                .AddSwaggerGen(c =>
                {
                    var options = config.GetSection(OpenApiOptions.SectionKey).Get<OpenApiOptions>() ?? new OpenApiOptions();

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
                    if (!string.IsNullOrWhiteSpace(options.Server))
                    {
                        c.AddServer(new OpenApiServer
                        {
                            Url = options.Server
                        });
                    }
                })
                .AddSwaggerGenNewtonsoftSupport()
                .AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

            return services;
        }

        private static void ConfigureProblemDetails(ProblemDetailsOptions options, IHostEnvironment environment)
        {
            Guard.Argument(environment, nameof(environment)).NotNull();

            options.IncludeExceptionDetails = (ctx, ex) => environment.IsDevelopment();
            options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
        }
    }
}
