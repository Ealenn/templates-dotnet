using Dawn;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Api.Dotnet.Template.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseSerilog(this IHostBuilder hostBuilder)
        {
            Guard.Argument(hostBuilder, nameof(hostBuilder)).NotNull();
            Serilog.Debugging.SelfLog.Enable(Console.Error);

            return hostBuilder
                .UseSerilog((hostingContext, services, loggerConfiguration) =>
                {
                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
                    loggerConfiguration.Enrich.FromLogContext();
                    loggerConfiguration.WriteTo.Console();

                    string seqServerUrl = hostingContext.Configuration["Logging:Seq:ServerUrl"];
                    string seqApiKey = hostingContext.Configuration["Logging:Seq:ApiKey"];
                    if (!string.IsNullOrWhiteSpace(seqServerUrl) && !string.IsNullOrWhiteSpace(seqApiKey))
                        loggerConfiguration.WriteTo.Seq(seqServerUrl, apiKey: seqApiKey);
                });
        }
    }
}
