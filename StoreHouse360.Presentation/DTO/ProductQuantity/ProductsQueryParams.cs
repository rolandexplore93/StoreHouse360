﻿using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Application.Queries.Products;
using StoreHouse360.DTO.Pagination;

namespace StoreHouse360.DTO.ProductQuantity
{
    public class ProductsQueryParams : PaginationRequestParams, IMapFrom<GetAllProductsQuery>
    {
        [FromQuery(Name = "name")]
        public string? Name { get; set; }
    }
}
