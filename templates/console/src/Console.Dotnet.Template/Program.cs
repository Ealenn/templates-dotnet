using Console.Dotnet.Template.Services;
using Console.Dotnet.Template.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System.Threading.Tasks;

namespace Console.Dotnet.Template
{
    static class Program
    {
        public static Task Main(string[] args)
        {
            return new HostBuilder()
                .UseSerilog((hostingContext, services, loggerConfiguration) =>
                {
                    loggerConfiguration
                        .Filter.ByIncludingOnly((logEvent) =>
                        {
                            if (logEvent.Level >= LogEventLevel.Error)
                                return true;

                            if (logEvent.Properties.TryGetValue("SourceContext", out var SourceContext))
                            {
                                var stringContext = SourceContext.ToString();
                                return stringContext.Contains("Console.Dotnet.Template");
                            }

                            return false;
                        })
                        .Enrich.FromLogContext()
                        .WriteTo.Console();
                })
                .ConfigureServices((context, services) =>
                {
                    services
                        .AddTransient<Application>()
                        .AddTransient<ICalculatorService, CalculatorService>();
                })
                .UseCocona(args, new[] { typeof(Application) })
                .UseConsoleLifetime()
                .Build()
                .RunAsync();
        }
    }
}
