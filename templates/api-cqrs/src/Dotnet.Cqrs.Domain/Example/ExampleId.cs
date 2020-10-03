using EventFlow.Core;

namespace Dotnet.Cqrs.Domain.Example
{
    public sealed class ExampleId : Identity<ExampleId>
    {
        public ExampleId(string value) : base(value)
        {
        }
    }
}