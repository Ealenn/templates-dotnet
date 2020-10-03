using EventFlow.Queries;
using System.Collections.Generic;

namespace Dotnet.Cqrs.ReadModel.Mongo.Shared.Queries
{
    public abstract class GetPaginatedQueryHandler<T> : IQuery<IEnumerable<T>>
    {
        public int Skip { get; }
        public int Take { get; }

        protected GetPaginatedQueryHandler(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }
    }
}
