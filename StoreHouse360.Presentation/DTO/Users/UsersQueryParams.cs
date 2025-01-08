using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Application.Queries.Users;
using StoreHouse360.DTO.Pagination;

namespace StoreHouse360.DTO.Users
{
    public class UsersQueryParams : PaginationRequestParams, IMapFrom<GetAllUsersQuery>
    {
        [FromQuery(Name = "username")]
        public string? UserName { get; set; }
    }
}
