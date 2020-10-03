using System.ComponentModel.DataAnnotations;

namespace Dotnet.Cqrs.Api.Controllers.Example.Models.Queries
{
    public class CreateExampleQuery
    {
        [Required]
        [StringLength(255, MinimumLength = 2)]
        [RegularExpression("^[a-z-éçèàâêîûôöïëäü]*$")]
        public string Name { get; set; }
        public bool Online { get; set; }
    }
}
