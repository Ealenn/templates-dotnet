namespace Dotnet.Cqrs.Api.Options
{
    public class CorsOptions
    {
        public const string SectionKey = "Cors";
        public string[] Methods { get; set; }
        public string[] Origins { get; set; }
        public string[] Headers { get; set; }
    }
}
