using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories.Aggregates;

namespace StoreHouse360.Application.Queries.UserRoles
{
    public class GetUserRolesQuery : GetPaginatedQuery<Role>
    {
        public int UserId { get; set; }
    }

    public class GetUserRolesQueryHandler : PaginatedQueryHandler<GetUserRolesQuery, Role>
    {
        private readonly IUserRolesRepository _userRolesRepository;

        public GetUserRolesQueryHandler(IUserRolesRepository userRolesRepository)
        {
            _userRolesRepository = userRolesRepository;
        }

        protected override async Task<IQueryable<Role>> GetQuery(GetUserRolesQuery request,
            CancellationToken cancellationToken)
        {
            var userRoles = await _userRolesRepository.FindByUserId(request.UserId);

            return new EnumerableQuery<Role>(userRoles.Roles);
        }
    }
}
