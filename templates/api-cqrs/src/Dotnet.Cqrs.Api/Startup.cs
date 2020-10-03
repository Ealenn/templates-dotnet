using AutoMapper;
using Dotnet.Cqrs.Api.Extensions;
using Dotnet.Cqrs.Service;
using EventFlow.AspNetCore.Extensions;
using EventFlow.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;
using Serilog;
using System.Globalization;

namespace Dotnet.Cqrs.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _hostEnvironment = hostEnvironment;
            _configuration = configuration;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            services.AddAppMvc(_hostEnvironment);
            services.AddAppHeaderForward(_hostEnvironment);
            services.AddAppOpenApi(_configuration);
            services.AddAppCors(_configuration);
            services.AddHealthChecks();

            services.AddAutoMapper(typeof(Startup).Assembly);

            ConfigureEventFlow(services);
        }

        public virtual void ConfigureEventFlow(IServiceCollection services)
        {
            services.AddEventFlow(o =>
            {
                o.UseServiceCollection(services);
                o.RegisterModule<ServiceModule>();
                o.AddAspNetCore();
            });
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("fr"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseSerilogRequestLogging();
            app.UseAppOpenApi();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseHttpMetrics();
            app.UseCors();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();
            app.UseEndpoints(endpoints =>
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
        }
    }
}
