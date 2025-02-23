using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.DTO.Common;

namespace StoreHouse360.DTO.Authorization.Policies
{
    public class PolicyVM : IViewModel, IMapFrom<Policy>
    {
        public Resource Resource { get; set; }
        public Method Method { get; set; }
        public string Name => Resource + "-" + Method;
    }
}
