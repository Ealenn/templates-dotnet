using Dotnet.Cqrs.Domain.Example;
using Dotnet.Cqrs.Domain.Example.Commands;
using Dotnet.Cqrs.Domain.Example.Entities;
using Dotnet.Cqrs.ReadModel.Mongo.Example.Queries;
using Dotnet.Cqrs.ReadModel.Mongo.Example.ReadModels;
using Dotnet.Cqrs.Service.Example.Abstractions;
using Dotnet.Cqrs.Service.Example.Models;
using EventFlow;
using EventFlow.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Dotnet.Cqrs.Service.Example
{
    public class ExampleService : IExampleService
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryProcessor _queryProcessor;

        public ExampleService(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
            _queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
        }

        public async Task<ExampleReadModel> CreateAsync(CreateExampleModel createExampleModel, CancellationToken cancellationToken)
        {
            var exampleId = ExampleId.New;
            var command = new CreateExampleCommand(exampleId, new ExampleEntity(exampleId)
            {
                Name = createExampleModel.Name,
                Online = createExampleModel.Online
            });
            await _commandBus.PublishAsync(command, cancellationToken);
            return await _queryProcessor.ProcessAsync(new GetExampleById(exampleId), cancellationToken);
        }

        public async Task<IEnumerable<ExampleReadModel>> GetAsync(int Skip, int Take, CancellationToken cancellationToken)
        {
            return await _queryProcessor.ProcessAsync(new GetExamples(Skip, Take), cancellationToken);
        }

        public async Task<ExampleReadModel> GetByIdAsync(ExampleId exampleId, CancellationToken cancellationToken)
        {
            return await _queryProcessor.ProcessAsync(new GetExampleById(exampleId), cancellationToken);
        }
    }
}
