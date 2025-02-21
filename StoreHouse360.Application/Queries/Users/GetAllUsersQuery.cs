using MediatR;
using StoreHouse360.Application.Common.QueryFilters;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Users
{
    [Authorize(Resource = Resource.Users, Method = Method.Read)]
    public class GetAllUsersQuery : GetPaginatedQuery<User>
    {
        [QueryFilter(QueryFilterCompareType.StringContains)]
        public string? UserName { get; set; }
    }

    public class GetAllUsersQueryHandler : PaginatedQueryHandler<GetAllUsersQuery, User>
    {

        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected override async Task<IQueryable<User>> GetQuery(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();
            return users;
        }
    }
}
