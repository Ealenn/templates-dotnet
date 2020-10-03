using Dotnet.Cqrs.Domain.Example.Entities;
using Dotnet.Cqrs.Domain.Example.Events;
using Dotnet.Cqrs.Domain.Shared.Specs;
using EventFlow.Aggregates;
using EventFlow.Extensions;
using EventFlow.Snapshots;
using EventFlow.Snapshots.Strategies;
using EventFlow.Specifications;
using System.Threading;
using System.Threading.Tasks;

namespace Dotnet.Cqrs.Domain.Example
{
    [AggregateName("Example")]
    public sealed class ExampleAggregate : SnapshotAggregateRoot<ExampleAggregate, ExampleId, ExampleSnapshot>
    {
        private const int SnapshotEveryVersion = 5;
        private readonly ExampleState _state = new ExampleState();

        #region Specifications
        private static readonly ISpecification<ExampleAggregate> IsNewSpecification = new AggregateIsNewSpecification<ExampleAggregate>();
        private static readonly ISpecification<ExampleAggregate> IsNotNewSpecification = new AggregateIsNotNewSpecification<ExampleAggregate>();
        #endregion

        public ExampleAggregate(ExampleId id) : base(id, SnapshotEveryFewVersionsStrategy.With(SnapshotEveryVersion))
        {
            Register(_state);
        }

        public void Create(ExampleEntity Example)
        {
            IsNewSpecification.ThrowDomainErrorIfNotSatisfied(this);
            Emit(new ExampleCreatedEvent(Example));
        }

        #region Snapshots
        protected override Task<ExampleSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
        {
            var snapshot = new ExampleSnapshot(_state.Example);
            return Task.FromResult(snapshot);
        }

        protected override Task LoadSnapshotAsync(ExampleSnapshot snapshot, ISnapshotMetadata metadata, CancellationToken cancellationToken)
        {
            _state.LoadSnapshot(snapshot);
            return Task.CompletedTask;
        }
        #endregion
    }
}
