using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mongo2Go;
using System;
using System.Collections.Generic;
using Xunit.Abstractions;
using Serilog;

namespace Dotnet.Cqrs.Api.Tests.Fake
{
    public class FakeApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private readonly MongoDbRunner _mongoDbRunner;

        public ITestOutputHelper Output { get; set; }
        public Guid CurrentTenant { get; set; }
        public Guid CurrentTeam { get; set; }

        public FakeApplicationFactory()
        {
            var binariesSearchDirectory = Environment.GetEnvironmentVariable("NUGET_PACKAGES");
            _mongoDbRunner = MongoDbRunner.Start(binariesSearchDirectory: binariesSearchDirectory);
        }

        ~FakeApplicationFactory()
        {
            _mongoDbRunner.Dispose();
        }

        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .UseEnvironment("Development")
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddXUnit(Output);
                })
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseStartup<TStartup>();
                })
                .ConfigureAppConfiguration(p =>
                {
                    p.AddInMemoryCollection(new Dictionary<string, string>
                    {
                        {"ConnectionStrings:mongodb",_mongoDbRunner.ConnectionString}
                    });
                })
                .UseSerilog();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(".");
            base.ConfigureWebHost(builder);
        }

        protected override void Dispose(bool disposing)
        {
            _mongoDbRunner?.Dispose();
            base.Dispose(disposing);
        }
    }
}
