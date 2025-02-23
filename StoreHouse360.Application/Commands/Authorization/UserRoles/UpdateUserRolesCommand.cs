using MediatR;
using StoreHouse360.Application.Common.QueryFilters;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Application.Repositories.Aggregates;

namespace StoreHouse360.Application.Commands.Authorization.UserRoles
{
    public class UpdateUserRolesCommand : IRequest
    {
        public int UserId { get; set; }

        [QueryFilter(QueryFilterCompareType.InArray, FieldName = "Id")]
        public IEnumerable<int> RoleIds { get; set; }
    }

    public class UpdateUserRolesCommandHandler : IRequestHandler<UpdateUserRolesCommand>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRolesRepository _userRolesRepository;

        public UpdateUserRolesCommandHandler(IRoleRepository roleRepository, IUserRolesRepository userRolesRepository)
        {
            _roleRepository = roleRepository;
            _userRolesRepository = userRolesRepository;
        }

        public async Task Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var userRoles = await _userRolesRepository.FindByUserId(request.UserId);

            var roles = _getRoles(request);

            userRoles.UpdateRoles(roles);

            await _userRolesRepository.Update(userRoles);
        }

        private IList<Role> _getRoles(UpdateUserRolesCommand request)
        {
            var roles = _roleRepository.GetAll().WhereFilters(request);

            return roles.ToList();
        }
    }
}
