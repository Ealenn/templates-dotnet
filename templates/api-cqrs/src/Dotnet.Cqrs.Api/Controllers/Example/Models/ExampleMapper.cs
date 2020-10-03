using AutoMapper;
using Dotnet.Cqrs.Api.Controllers.Example.Models.Queries;
using Dotnet.Cqrs.Api.Controllers.Example.Models.Responses;
using Dotnet.Cqrs.Domain.Example;
using Dotnet.Cqrs.ReadModel.Mongo.Example.ReadModels;
using Dotnet.Cqrs.Service.Example.Models;

namespace Dotnet.Cqrs.Api.Controllers.Example.Models
{
    public class ExampleMapper : Profile
    {
        public ExampleMapper()
        {
            CreateMap<ExampleReadModel, GetExampleResponse>()
                .ForMember(m => m.Id, m => m.MapFrom(p => ExampleId.With(p.Id).GetGuid()))
                .ForMember(m => m.Name, m => m.MapFrom(p => p.Name))
                .ForMember(m => m.Online, m => m.MapFrom(p => p.Online));

            CreateMap<CreateExampleQuery, CreateExampleModel>()
                .ForMember(m => m.Name, m => m.MapFrom(p => p.Name))
                .ForMember(m => m.Online, m => m.MapFrom(p => p.Online));
        }
    }
}
