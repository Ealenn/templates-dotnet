using Api.Dotnet.Template.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Api.Dotnet.Template
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .UseSerilog()
                .Build()
                .Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                        webBuilder.UseKestrel(options => options.AddServerHeader = false);
                    });
    }
}
