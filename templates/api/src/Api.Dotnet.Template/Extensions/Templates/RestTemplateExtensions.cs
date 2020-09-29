using Dawn;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;
using Serilog;
using System;

namespace Api.Dotnet.Template.Extensions.Templates
{
    public static class RestTemplateExtensions
    {
        public static IServiceCollection AddRestTemplate(this IServiceCollection services
            , Action<IServiceCollection, IServiceProvider> preHook = null
            , Action<IServiceCollection, IHealthChecksBuilder> healthCheckHook = null)
        {
            Guard.Argument(services, nameof(services)).NotNull();

            using (var scope = services.BuildServiceProvider().GetService<IServiceScopeFactory>().CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var hostingEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

                preHook?.Invoke(services, serviceProvider);

                services.AddMvc(hostingEnvironment);
                services.AddCors(configuration);
                services.AddOpenApi(configuration);
                services.AddHeaderForward(hostingEnvironment);

                var healthChecksBuilder = services.AddHealthChecks();
                healthCheckHook?.Invoke(services, healthChecksBuilder);
            }

            return services;
        }

        public static IApplicationBuilder UseRestTemplate(this IApplicationBuilder applicationBuilder)
        {
            Guard.Argument(applicationBuilder, nameof(applicationBuilder)).NotNull();

            applicationBuilder.UseExceptionHandler();
            applicationBuilder.UseSerilogRequestLogging();
            applicationBuilder.UseOpenApi();
            applicationBuilder.UseStaticFiles();
            applicationBuilder.UseRouting();
            applicationBuilder.UseHttpMetrics();
            applicationBuilder.UseCors();

            applicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapMetrics();
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
                {
                    AllowCachingResponses = false,
                    Predicate = check => check.Tags.Contains("ready")
                });
                endpoints.MapHealthChecks("/health/live", new HealthCheckOptions
                {
                    AllowCachingResponses = false,
                    Predicate = _ => false
                });
            });

            return applicationBuilder;
        }
    }
}
