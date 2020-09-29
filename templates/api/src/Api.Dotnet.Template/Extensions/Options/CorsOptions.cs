namespace Api.Dotnet.Template.Extensions.Options
{
    public class CorsOptions
    {
        public const string OptionSection = "Features:Cors";
        public string[] Methods { get; set; }
        public string[] Origins { get; set; }
        public string[] Headers { get; set; }
    }
}
