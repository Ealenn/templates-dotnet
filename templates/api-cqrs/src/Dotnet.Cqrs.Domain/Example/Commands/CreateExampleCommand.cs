using Dotnet.Cqrs.Domain.Example.Entities;
using EventFlow.Commands;

namespace Dotnet.Cqrs.Domain.Example.Commands
{
    [CommandVersion(nameof(CreateExampleCommand), 1)]
    public sealed class CreateExampleCommand : Command<ExampleAggregate, ExampleId>
    {
        public ExampleEntity Example { get; }

        public CreateExampleCommand(ExampleId aggregateId, ExampleEntity example)
             : base(aggregateId)
        {
            Example = example;
        }
    }
}
