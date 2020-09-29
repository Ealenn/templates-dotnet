namespace Api.Dotnet.Template.Extensions.Options
{
    public class OpenApiOptions
    {
        public const string OptionSection = "Features:OpenApi";
        public string Version { get; set; } = "v1";
        public string Title { get; set; } = "API";
        public string Logo { get; set; } = "/img/logo/logo-512.png";
        public string Description { get; set; } = "An application with Swagger, Swashbuckle, and API versioning.";
        public string ContactName { get; set; } = "Ealen";
        public string ContactEmail { get; set; } = "contact@ealen.dev";
        public bool EnableRedoc { get; set; } = true;
        public bool EnableSwaggerUi { get; set; } = true;
    }
}
