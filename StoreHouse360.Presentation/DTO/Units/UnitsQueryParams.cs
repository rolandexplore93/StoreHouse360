using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Application.Queries.Units;
using StoreHouse360.DTO.Pagination;

namespace StoreHouse360.DTO.Units
{
    public class UnitsQueryParams : PaginationRequestParams, IMapFrom<GetAllUnitsQuery>
    {
        [FromQuery(Name = "name")]
        public string? Name { get; set; }
    }
}
