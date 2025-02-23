using MediatR;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories;

namespace StoreHouse360.Application.Commands.Roles
{
    public class UpdateRoleCommand : IRequest<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Permissions Permissions { get; set; }
    }

    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, int>
    {
        private readonly IRoleRepository _roleRepository;

        public UpdateRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<int> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.Update(new Role(request.Id, request.Name, request.Permissions));

            return role.Id;
        }
    }
}
