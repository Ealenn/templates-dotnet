using EventFlow.Aggregates;
using EventFlow.Specifications;
using System.Collections.Generic;

namespace Dotnet.Cqrs.Domain.Shared.Specs
{
    public sealed class AggregateIsNewSpecification<TAggregateRoot> : Specification<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
    {
        protected override IEnumerable<string> IsNotSatisfiedBecause(TAggregateRoot obj)
        {
            if (!obj.IsNew)
                yield return "Aggregate should be new";
        }
    }
}
