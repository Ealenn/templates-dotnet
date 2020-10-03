using Dotnet.Cqrs.Domain;
using Dotnet.Cqrs.Infrastructure.EventStore;
using Dotnet.Cqrs.ReadModel.Mongo;
using Dotnet.Cqrs.Service.Example;
using Dotnet.Cqrs.Service.Example.Abstractions;
using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;

namespace Dotnet.Cqrs.Service
{
    public sealed class ServiceModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions.AddDefaults(typeof(ServiceModule).Assembly)
                .RegisterModule<DomainModule>()
                .RegisterModule<InfrastructureEventStoreModule>()
                .RegisterModule<ReadModelMongoModule>()
                .RegisterServices(s =>
                {
                    s.Register<IExampleService, ExampleService>();
                });
        }
    }
}
