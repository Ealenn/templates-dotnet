using Dotnet.Cqrs.Infrastructure.EventStore;
using Dotnet.Cqrs.Service;
using EventFlow.AspNetCore.Extensions;
using EventFlow.DependencyInjection.Extensions;
using EventFlow.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.Cqrs.Api.Tests.Fake
{
    public class FakeStartup : Startup
    {
        public FakeStartup(IWebHostEnvironment hostEnvironment, IConfiguration configuration)
            : base(hostEnvironment, configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddHealthChecks()
                .AddCheck<FakeHealthCheck>(nameof(FakeHealthCheck), tags: new[] { "ready" });
        }

        public override void ConfigureEventFlow(IServiceCollection services)
        {
            services.AddEventFlow(o =>
            {
                o.UseServiceCollection(services);
                o.RegisterModule<ServiceModule>();
                o.RegisterServices(register =>
                {
                    register.Register<IDbContextProvider<EventStoreDbContext>, FakedEventStoreContextProvider>();
                });
                o.AddAspNetCore();
            });
        }
    }
}
