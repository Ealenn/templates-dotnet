using Api.Dotnet.Template.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace Api.Dotnet.Template.IntegrationTests.Fake
{
    public class FakeApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .UseSerilog()
                .UseEnvironment("Development")
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseStartup<TStartup>();
                });
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.UseContentRoot(".");
        }
    }
}
