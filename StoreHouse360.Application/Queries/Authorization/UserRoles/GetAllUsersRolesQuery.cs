using MediatR;
using StoreHouse360.Application.Common.Models;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories.Aggregates;

namespace StoreHouse360.Application.Queries.Authorization.UserRoles
{
    [Authorize(Method = Method.Read, Resource = Resource.Roles)]
    public class GetAllUsersRolesQuery : IRequest<IPaginatedCollections<Application.Common.Security.UserRoles>>
    {

    }

    public class GetAllUsersRolesQueryHandler : IRequestHandler<GetAllUsersRolesQuery, IPaginatedCollections<Application.Common.Security.UserRoles>>
    {
        private readonly IUserRolesRepository _userRolesRepository;

        public GetAllUsersRolesQueryHandler(IUserRolesRepository userRolesRepository)
        {
            _userRolesRepository = userRolesRepository;
        }

        public Task<IPaginatedCollections<Application.Common.Security.UserRoles>> Handle(GetAllUsersRolesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_userRolesRepository.GetAll().AsPaginatedQuery());
        }
    }
}
