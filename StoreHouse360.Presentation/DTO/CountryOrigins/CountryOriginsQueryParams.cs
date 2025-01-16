using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Application.Queries.CountryOrigins;
using StoreHouse360.DTO.Pagination;

namespace StoreHouse360.DTO.CountryOrigins
{
    public class CountryOriginsQueryParams : PaginationRequestParams, IMapFrom<GetAllCountryOriginsQuery>
    {
        [FromQuery(Name = "name")]
        public string? Name { get; set; }
    }
}
