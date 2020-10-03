using Dotnet.Cqrs.Domain.Example.Commands;
using EventFlow.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Dotnet.Cqrs.Domain.Example.CommandHandlers
{
    public sealed class CreateExampleCommandHandler : CommandHandler<ExampleAggregate, ExampleId, CreateExampleCommand>
    {
        public override Task ExecuteAsync(ExampleAggregate aggregate, CreateExampleCommand command, CancellationToken cancellationToken)
        {
            aggregate.Create(command.Example);
            return Task.CompletedTask;
        }
    }
}
