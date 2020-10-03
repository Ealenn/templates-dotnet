using Dotnet.Cqrs.Domain.Example;
using Dotnet.Cqrs.Domain.Example.Events;
using EventFlow.Aggregates;
using EventFlow.MongoDB.ReadStores;
using EventFlow.MongoDB.ReadStores.Attributes;
using EventFlow.ReadStores;

namespace Dotnet.Cqrs.ReadModel.Mongo.Example.ReadModels
{
    [MongoDbCollectionName("Example")]
    public sealed class ExampleReadModel : IMongoDbReadModel,
        IAmReadModelFor<ExampleAggregate, ExampleId, ExampleCreatedEvent>
    {
        public string Id { get; set; }
        public long? Version { get; set; }
        public string Name { get; set; }
        public bool Online { get; set; }

        public void Apply(IReadModelContext context, IDomainEvent<ExampleAggregate, ExampleId, ExampleCreatedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity.Value;
            Name = domainEvent.AggregateEvent.ExampleEntity.Name;
            Online = domainEvent.AggregateEvent.ExampleEntity.Online;
        }
    }
}
