using MediatR;
using StoreHouse360.Application.Common.Models;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories.Aggregates;

namespace StoreHouse360.Application.Queries.Authorization.UserRoles
{
    public class GetUserRolesQuery : GetPaginatedQuery<Role>
    {
        public int UserId { get; set; }
    }

    public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, IPaginatedCollections<Role>>
    {
        private readonly IUserRolesRepository _userRolesRepository;

        public GetUserRolesQueryHandler(IUserRolesRepository userRolesRepository)
        {
            _userRolesRepository = userRolesRepository;
        }

        public async Task<IPaginatedCollections<Role>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            var userRoles = await _userRolesRepository.FindByUserId(request.UserId);

            return new EnumerableQuery<Role>(userRoles.Roles).AsPaginatedQuery();
        }
    }
}
