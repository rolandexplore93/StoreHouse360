using StoreHouse360.Application.Commands.Roles;
using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.DTO.Authorization.Permissions;

namespace StoreHouse360.DTO.Authorization.Roles
{
    public class CreateRoleRequestDTO : IMapFrom<CreateRoleCommand>
    {
        public string Name { get; set; }
        public PermissionsVM Permissions { get; set; }
    }
}
