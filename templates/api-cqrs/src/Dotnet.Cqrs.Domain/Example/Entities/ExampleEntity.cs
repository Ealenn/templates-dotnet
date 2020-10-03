using EventFlow.Entities;

namespace Dotnet.Cqrs.Domain.Example.Entities
{
    public sealed class ExampleEntity : Entity<ExampleId>
    {
        public string Name { get; set; }
        public bool Online { get; set; }
        public ExampleEntity(ExampleId id) : base(id)
        {
        }
    }
}