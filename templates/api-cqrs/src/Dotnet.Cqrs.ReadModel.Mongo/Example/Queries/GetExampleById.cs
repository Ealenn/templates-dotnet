using Dotnet.Cqrs.Domain.Example;
using Dotnet.Cqrs.ReadModel.Mongo.Example.ReadModels;
using EventFlow.Queries;

namespace Dotnet.Cqrs.ReadModel.Mongo.Example.Queries
{
    public class GetExampleById : IQuery<ExampleReadModel>
    {
        public ExampleId ExampleId { get; }
        public GetExampleById(ExampleId exampleId)
        {
            ExampleId = exampleId;
        }
    }
}
