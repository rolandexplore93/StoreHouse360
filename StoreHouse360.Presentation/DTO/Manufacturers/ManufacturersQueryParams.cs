using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Application.Queries.Manufacturers;
using StoreHouse360.DTO.Pagination;

namespace StoreHouse360.DTO.Manufacturers
{
    public class ManufacturersQueryParams : PaginationRequestParams, IMapFrom<GetAllManufacturersQuery>
    {
        [FromQuery(Name = "name")]
        public string? Name { get; set; }
    }
}
