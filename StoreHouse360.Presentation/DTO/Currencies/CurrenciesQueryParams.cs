using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Application.Queries.Currencies;
using StoreHouse360.DTO.Pagination;

namespace StoreHouse360.DTO.Currencies
{
    public class CurrenciesQueryParams : PaginationRequestParams, IMapFrom<GetAllCurrenciesQuery>
    {
        [FromQuery(Name = "name")]
        public string? Name { get; set; }
    }
}
