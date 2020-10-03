using System;
using System.ComponentModel.DataAnnotations;

namespace Dotnet.Cqrs.Api.Controllers.Shared.Queries
{
    public class PaginatedQuery
    {
        [Range(0, int.MaxValue)]
        public int Skip { get; set; }

        [Range(1, 25)]
        public int Take { get; set; }
    }
}
