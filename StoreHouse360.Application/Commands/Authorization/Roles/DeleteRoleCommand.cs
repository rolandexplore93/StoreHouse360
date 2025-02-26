using MediatR;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories;

namespace StoreHouse360.Application.Commands.Authorization.Roles
{
    [Authorize(Method = Method.Delete, Resource = Resource.Roles)]
    public class DeleteRoleCommand : IRequest
    {
        public int Id;
    }

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
    {
        private readonly IRoleRepository _roleRepository;

        public DeleteRoleCommandHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            await _roleRepository.DeleteAsync(request.Id);
        }
    }
}
