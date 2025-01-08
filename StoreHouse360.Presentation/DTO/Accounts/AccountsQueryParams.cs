using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Application.Queries.Accounts;
using StoreHouse360.DTO.Pagination;

namespace StoreHouse360.DTO.Accounts
{
    public class AccountsQueryParams : PaginationRequestParams, IMapFrom<GetAllAccountsQuery>
    {
        [FromQuery(Name = "name")]
        public string? Name { get; set; }
    }
}
