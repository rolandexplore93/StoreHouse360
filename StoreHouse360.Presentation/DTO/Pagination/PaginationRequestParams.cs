using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Queries.Common;

namespace StoreHouse360.DTO.Pagination
{
    public class PaginationRequestParams
    {
        [FromQuery(Name = "current_page")] public int Page { get; set; } = 1;
        [FromQuery(Name = "page_size")] public int PageSize { get; set; } = 10;
    }

    public static class PaginationParamsExtension
    {
        public static T AsQuery<T>(this PaginationRequestParams requestParams) where T : IGetPaginatedQuery
        {
            return requestParams.AsQuery(Activator.CreateInstance<T>());
        }

        public static T AsQuery<T>(this PaginationRequestParams requestParams, T query) where T : IGetPaginatedQuery
        {
            query.Page = requestParams.Page;
            query.PageSize = requestParams.PageSize;
            return query;
        }
    }
}
