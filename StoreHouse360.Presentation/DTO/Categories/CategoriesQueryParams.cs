using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Application.Queries.Categories;
using StoreHouse360.DTO.Pagination;

namespace StoreHouse360.DTO.Categories
{
    public class CategoriesQueryParams : PaginationRequestParams, IMapFrom<GetAllCategoriesQuery>
    {
        [FromQuery(Name = "name")]
        public string? Name { get; set; }
    }
}
