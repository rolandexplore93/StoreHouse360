using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Application.Queries.Warehouses;
using StoreHouse360.DTO.Pagination;

namespace StoreHouse360.DTO.Warehouses
{
    public class WarehousesQueryParams : PaginationRequestParams, IMapFrom<GetAllWarehousesQuery>
    {
        [FromQuery(Name = "name")]
        public string? Name { get; set; }
    }
}
