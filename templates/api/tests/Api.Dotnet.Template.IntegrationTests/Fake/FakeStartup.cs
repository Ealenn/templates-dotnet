using Api.Dotnet.Template.Extensions.Templates;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Api.Dotnet.Template.IntegrationTests.Fake
{
    public class FakeStartup
    {
        public IConfiguration Configuration { get; }

        public FakeStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRestTemplate(healthCheckHook: HealthCheckHook, preHook: (serviceCollection, _) =>
            {
                serviceCollection.AddRazorPages().AddApplicationPart(Assembly.GetAssembly(typeof(Program)));
            });

            services.AddSingleton(AutoMapperService());
            services.AddHttpClient();
        }

        private IMapper AutoMapperService() =>
            new MapperConfiguration(opt => opt.AddMaps(Assembly.GetAssembly(typeof(Program))))
                .CreateMapper();

        public void Configure(IApplicationBuilder app)
        {
            app.UseRestTemplate();
        }

        private void HealthCheckHook(IServiceCollection services, IHealthChecksBuilder healthChecksBuilder)
        {
            healthChecksBuilder.AddCheck<FakeHealthCheck>(nameof(FakeHealthCheck), tags: new[] { "ready" });
        }
    }
}
