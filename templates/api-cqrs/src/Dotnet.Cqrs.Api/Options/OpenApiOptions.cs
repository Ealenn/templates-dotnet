namespace Dotnet.Cqrs.Api.Options
{
    public class OpenApiOptions
    {
        public const string SectionKey = "Swagger";
        public string Version { get; set; } = "v1";
        public string Title { get; set; } = "Garden Assistant";
        public string Description { get; set; } = "";
        public string ContactName { get; set; } = "Ealen";
        public string ContactEmail { get; set; } = "";
        public string Logo { get; set; } = "/img/logo/logo-512.png";
        public string Server { get; set; } = "";
    }
}
