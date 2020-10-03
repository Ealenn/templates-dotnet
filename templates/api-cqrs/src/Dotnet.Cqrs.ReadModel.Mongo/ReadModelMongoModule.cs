using Dotnet.Cqrs.ReadModel.Mongo.Example.ReadModels;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;
using EventFlow.MongoDB.EventStore;
using EventFlow.MongoDB.Extensions;
using EventFlow.MongoDB.ReadStores;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Dotnet.Cqrs.ReadModel.Mongo
{
    public sealed class ReadModelMongoModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions
                .AddDefaults(typeof(ReadModelMongoModule).Assembly)
                .RegisterServices(sr =>
                {
                    sr.Register(f =>
                    {
                        var configuration = f.Resolver.Resolve<IConfiguration>();
                        var connectionString = configuration.GetConnectionString("mongodb");
                        var mongoDatabase = new MongoClient(connectionString).GetDatabase("read-model");
                        return mongoDatabase;
                    }, Lifetime.Singleton);
                    sr.Register<IReadModelDescriptionProvider, ReadModelDescriptionProvider>(Lifetime.Singleton, true);
                    sr.Register<IMongoDbEventSequenceStore, MongoDbEventSequenceStore>(Lifetime.Singleton);
                })
                .UseMongoDbReadModel<ExampleReadModel>();
        }
    }
}
