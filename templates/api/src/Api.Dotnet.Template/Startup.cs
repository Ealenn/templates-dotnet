using Api.Dotnet.Template.Extensions.Templates;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace Api.Dotnet.Template
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRestTemplate();
            services.AddAutoMapper(GetType().Assembly);
        }

        public void Configure(IApplicationBuilder app)
        {
            var supportedCultures = new[]
            {
                new CultureInfo("fr-FR"),
                new CultureInfo("fr"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("fr-FR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseRestTemplate();
            app.UseStaticFiles();
        }
    }
}
