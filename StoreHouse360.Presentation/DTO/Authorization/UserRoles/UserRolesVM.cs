using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.DTO.Authorization.Roles;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Users;

namespace StoreHouse360.DTO.Authorization.UserRoles
{
    public class UserRolesVM : IViewModel, IMapFrom<Application.Common.Security.UserRoles>
    {
        public UserVM User { get; set; }
        public IList<RoleVM> Roles { get; set; }
    }
}
