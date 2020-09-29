namespace Api.Dotnet.Template.Extensions.ProblemDetails
{
    public class ApiProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public string Message { get; set; }
        public string Code { get; set; }
    }
}
