using EventFlow;
using EventFlow.Configuration;
using EventFlow.Extensions;

namespace Dotnet.Cqrs.Domain
{
    public sealed class DomainModule : IModule
    {
        public void Register(IEventFlowOptions eventFlowOptions)
        {
            eventFlowOptions.AddDefaults(typeof(DomainModule).Assembly);
        }
    }
}
