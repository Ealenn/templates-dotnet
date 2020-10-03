using Dotnet.Cqrs.ReadModel.Mongo.Example.Queries;
using Dotnet.Cqrs.ReadModel.Mongo.Example.ReadModels;
using EventFlow.MongoDB.ReadStores;
using EventFlow.Queries;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dotnet.Cqrs.ReadModel.Mongo.Example.QueryHandlers
{
    public class GetExampleByIdHandler : IQueryHandler<GetExampleById, ExampleReadModel>
    {
        private readonly IMongoDbReadModelStore<ExampleReadModel> _readModelStore;

        public GetExampleByIdHandler(IMongoDbReadModelStore<ExampleReadModel> readModelStore)
        {
            _readModelStore = readModelStore ?? throw new ArgumentNullException(nameof(readModelStore));
        }

        public async Task<ExampleReadModel> ExecuteQueryAsync(GetExampleById query, CancellationToken cancellationToken)
        {
            var cursor = await _readModelStore.FindAsync(f => f.Id == query.ExampleId.Value, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
