using EventFlow.Aggregates;
using EventFlow.Specifications;
using System.Collections.Generic;

namespace Dotnet.Cqrs.Domain.Shared.Specs
{
    public sealed class AggregateIsNotNewSpecification<TAggregateRoot> : Specification<TAggregateRoot>
        where TAggregateRoot : IAggregateRoot
    {
        protected override IEnumerable<string> IsNotSatisfiedBecause(TAggregateRoot obj)
        {
            if (obj.IsNew)
                yield return "Aggregate should not be new";
        }
    }
}
