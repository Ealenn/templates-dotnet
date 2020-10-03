using Dotnet.Cqrs.Domain.Example;
using Dotnet.Cqrs.ReadModel.Mongo.Example.ReadModels;
using Dotnet.Cqrs.Service.Example.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Dotnet.Cqrs.Service.Example.Abstractions
{
    public interface IExampleService
    {
        Task<IEnumerable<ExampleReadModel>> GetAsync(int Skip, int Take, CancellationToken cancellationToken);
        Task<ExampleReadModel> GetByIdAsync(ExampleId exampleId, CancellationToken cancellationToken);
        Task<ExampleReadModel> CreateAsync(CreateExampleModel createExampleModel, CancellationToken cancellationToken);
    }
}
