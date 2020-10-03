using EventFlow.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Dotnet.Cqrs.Infrastructure.EventStore
{
    internal sealed class EventStoreDbContextProvider : IDbContextProvider<EventStoreDbContext>
    {
        private readonly DbContextOptions<EventStoreDbContext> _options;

        public EventStoreDbContextProvider(IConfiguration configuration)
        {
            _options = new DbContextOptionsBuilder<EventStoreDbContext>()
                .UseNpgsql(configuration.GetConnectionString("eventstore"))
                .Options;
        }

        public EventStoreDbContext CreateContext()
        {
            var context = new EventStoreDbContext(_options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
