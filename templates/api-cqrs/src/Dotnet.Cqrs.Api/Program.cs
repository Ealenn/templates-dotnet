using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Dotnet.Cqrs.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Serilog.Debugging.SelfLog.Enable(Console.Error);
            CreateHostBuilder(args)
                .UseSerilog((hostingContext, services, loggerConfiguration) =>
                {
                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
                    loggerConfiguration.Enrich.FromLogContext();
                    loggerConfiguration.WriteTo.Console();

                    string seqServerUrl = hostingContext.Configuration["Logging:Seq:ServerUrl"];
                    string seqApiKey = hostingContext.Configuration["Logging:Seq:ApiKey"];
                    if (!string.IsNullOrWhiteSpace(seqServerUrl) && !string.IsNullOrWhiteSpace(seqApiKey))
                        loggerConfiguration.WriteTo.Seq(seqServerUrl, apiKey: seqApiKey);
                })
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
