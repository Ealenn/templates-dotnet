using Dotnet.Cqrs.ReadModel.Mongo.Example.ReadModels;
using Dotnet.Cqrs.ReadModel.Mongo.Shared.Queries;

namespace Dotnet.Cqrs.ReadModel.Mongo.Example.Queries
{
    public class GetExamples : GetPaginatedQueryHandler<ExampleReadModel>
    {
        public GetExamples(int skip, int take)
            : base(skip, take)
        {
        }
    }
}
