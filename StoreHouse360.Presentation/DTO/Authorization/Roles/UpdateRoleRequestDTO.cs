using StoreHouse360.Application.Commands.Authorization.Roles;
using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.DTO.Authorization.Permissions;

namespace StoreHouse360.DTO.Authorization.Roles
{
    public class UpdateRoleRequestDTO : IMapFrom<UpdateRoleCommand>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PermissionsVM Permissions { get; set; }
    }
}
