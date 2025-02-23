using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.DTO.Authorization.Permissions;
using StoreHouse360.DTO.Common;

namespace StoreHouse360.DTO.Authorization.Roles
{
    public class RoleVM : IViewModel, IMapFrom<Role>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PermissionsVM Permissions { get; set; }
    }
}
