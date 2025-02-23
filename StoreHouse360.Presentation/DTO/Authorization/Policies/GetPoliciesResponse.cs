using StoreHouse360.Application.Common.Security;

namespace StoreHouse360.DTO.Authorization.Policies
{
    public class GetPoliciesResponse
    {
        public IEnumerable<Method> Methods { get; set; }
        public IEnumerable<Resource> Resources { get; set; }
    }
}
