using Dotnet.Cqrs.Domain.Example.Entities;
using EventFlow.Aggregates;
using EventFlow.EventStores;

namespace Dotnet.Cqrs.Domain.Example.Events
{
    [EventVersion(nameof(ExampleCreatedEvent), 1)]
    public sealed class ExampleCreatedEvent : AggregateEvent<ExampleAggregate, ExampleId>
    {
        public ExampleEntity ExampleEntity { get; }

        public ExampleCreatedEvent(ExampleEntity exampleEntity)
        {
            ExampleEntity = exampleEntity;
        }
    }
}
