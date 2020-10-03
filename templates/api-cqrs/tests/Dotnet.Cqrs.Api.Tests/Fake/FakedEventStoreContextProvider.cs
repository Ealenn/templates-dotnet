using Dotnet.Cqrs.Infrastructure.EventStore;
using EventFlow.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Cqrs.Api.Tests.Fake
{
    public class FakedEventStoreContextProvider : IDbContextProvider<EventStoreDbContext>
    {
        public EventStoreDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<EventStoreDbContext>()
                .UseInMemoryDatabase($"event-store")
                .Options;

            return new EventStoreDbContext(options);
        }
    }
}
