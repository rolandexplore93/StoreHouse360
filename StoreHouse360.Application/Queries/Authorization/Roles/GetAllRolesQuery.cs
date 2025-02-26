using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;

namespace StoreHouse360.Application.Queries.Authorization.Roles
{
    [Authorize(Method = Method.Read, Resource = Resource.Roles)]
    public class GetAllRolesQuery : GetPaginatedQuery<Role>
    {
    }

    public class GetAllRolesQueryHandler : PaginatedQueryHandler<GetAllRolesQuery, Role>
    {
        private readonly IRoleRepository _roleRepository;

        public GetAllRolesQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        protected override Task<IQueryable<Role>> GetQuery(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_roleRepository.GetAll());
        }
    }
}
