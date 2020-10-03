using System;

namespace Dotnet.Cqrs.Api.Controllers.Example.Models.Responses
{
    public class GetExampleResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Online { get; set; }
    }
}
