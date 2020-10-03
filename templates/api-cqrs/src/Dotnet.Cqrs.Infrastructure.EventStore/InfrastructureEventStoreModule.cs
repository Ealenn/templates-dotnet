using EventFlow;
using EventFlow.Configuration;
using EventFlow.EntityFramework;
using EventFlow.EntityFramework.Extensions;
using EventFlow.Extensions;

namespace Dotnet.Cqrs.Infrastructure.EventStore
{
    public sealed class InfrastructureEventStoreModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions.AddDefaults(typeof(InfrastructureEventStoreModule).Assembly)
                .ConfigureEntityFramework(EntityFrameworkConfiguration.New)
                .AddDbContextProvider<EventStoreDbContext, EventStoreDbContextProvider>()
                .UseEntityFrameworkEventStore<EventStoreDbContext>()
                .UseEntityFrameworkSnapshotStore<EventStoreDbContext>();
        }
    }
}
