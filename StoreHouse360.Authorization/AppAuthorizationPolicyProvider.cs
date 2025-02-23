using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using StoreHouse360.Application.Common.Security;

namespace StoreHouse360.Authorization
{
    public class AppAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly AuthorizationOptions _options;

        public AppAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
            _options = options.Value;
        }

        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            var policy = await base.GetPolicyAsync(policyName);

            if (policy != null || !PolicyHelper.IsValidPolicy(policyName)) return policy;

            var policies = PolicyHelper.CreatePoliciesFromString(policyName);

            policy = new AuthorizationPolicyBuilder()
                .AddRequirements(new PoliciesAuthorizationRequirement(policies))
                .Build();

            _options.AddPolicy(policyName, policy);

            return policy;
        }
    }
}
