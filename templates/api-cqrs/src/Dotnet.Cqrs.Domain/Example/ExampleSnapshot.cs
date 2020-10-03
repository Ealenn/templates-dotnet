using Dotnet.Cqrs.Domain.Example.Entities;
using EventFlow.Snapshots;

namespace Dotnet.Cqrs.Domain.Example
{
    [SnapshotVersion(nameof(ExampleSnapshot), 1)]
    public sealed class ExampleSnapshot : ISnapshot
    {
        public ExampleEntity Example { get; }

        public ExampleSnapshot(ExampleEntity Example)
        {
            Example = Example;
        }
    }
}
