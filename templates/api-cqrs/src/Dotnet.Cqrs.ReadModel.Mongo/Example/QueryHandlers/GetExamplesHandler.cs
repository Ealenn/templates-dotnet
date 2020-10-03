using Dotnet.Cqrs.ReadModel.Mongo.Example.Queries;
using Dotnet.Cqrs.ReadModel.Mongo.Example.ReadModels;
using EventFlow.MongoDB.ReadStores;
using EventFlow.Queries;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Dotnet.Cqrs.ReadModel.Mongo.Example.QueryHandlers
{
    public class GetExamplesHandler : IQueryHandler<GetExamples, IEnumerable<ExampleReadModel>>
    {
        private readonly IMongoDbReadModelStore<ExampleReadModel> _readModelStore;

        public GetExamplesHandler(IMongoDbReadModelStore<ExampleReadModel> readModelStore)
        {
            _readModelStore = readModelStore ?? throw new ArgumentNullException(nameof(readModelStore));
        }

        public async Task<IEnumerable<ExampleReadModel>> ExecuteQueryAsync(GetExamples query, CancellationToken cancellationToken)
        {
            var result = await _readModelStore.FindAsync(x => x.Online == true, new FindOptions<ExampleReadModel, ExampleReadModel>()
            {
                Skip = query.Skip,
                BatchSize = query.Take,
                Limit = query.Take
            }, cancellationToken);
            return result.ToList();
        }
    }
}
