using MediatR;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories;

namespace StoreHouse360.Application.Commands.Authorization.Roles
{
    [Authorize(Method = Method.Write, Resource = Resource.Roles)]
    public class CreateRoleCommand : IRequest<int>
    {
        public string Name { get; set; }
        public Permissions Permissions { get; set; }
    }

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, int>
    {
        private readonly IRoleRepository _roleRepository;

        public CreateRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var saveAction = await _roleRepository.CreateAsync(new Role(request.Name, request.Permissions));

            var role = await saveAction();

            return role.Id;
        }
    }
}
