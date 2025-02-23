using Microsoft.AspNetCore.Authorization;
using StoreHouse360.Application.Common.Security;

namespace StoreHouse360.Authorization
{
    public class PoliciesAuthorizationRequirement : IAuthorizationRequirement
    {
        public IEnumerable<Policy> Policies { get; }
        public PoliciesAuthorizationRequirement(IEnumerable<Policy> policies)
        {
            Policies = policies;
        }
    }
}
