using EventFlow.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Cqrs.Infrastructure.EventStore
{
    public sealed class EventStoreDbContext : DbContext
    {
        public EventStoreDbContext(DbContextOptions<EventStoreDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .AddEventFlowEvents()
                .AddEventFlowSnapshots();
        }
    }
}
