using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.DTO.Authorization.Policies;
using StoreHouse360.DTO.Common;

namespace StoreHouse360.DTO.Authorization.Permissions
{
    public class PermissionsVM : IViewModel, IMapFrom<Application.Common.Security.Permissions>
    {
        public bool AllPermissions { get; set; }
        public bool None { get; set; }
        public IList<PolicyVM> Policies { get; set; }
    }
}
