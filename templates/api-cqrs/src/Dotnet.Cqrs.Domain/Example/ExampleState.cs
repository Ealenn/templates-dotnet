using Dotnet.Cqrs.Domain.Example.Entities;
using Dotnet.Cqrs.Domain.Example.Events;
using EventFlow.Aggregates;

namespace Dotnet.Cqrs.Domain.Example
{
    public sealed class ExampleState : AggregateState<ExampleAggregate, ExampleId, ExampleState>,
        IApply<ExampleCreatedEvent>
    {
        public ExampleEntity Example { get; private set; }

        public void Apply(ExampleCreatedEvent aggregateEvent)
        {
            Example = aggregateEvent.ExampleEntity;
        }

        public void LoadSnapshot(ExampleSnapshot snapshot)
        {
            Example = snapshot.Example;
        }
    }
}