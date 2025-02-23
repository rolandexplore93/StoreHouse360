using Microsoft.AspNetCore.Authorization;
using StoreHouse360.Application.Common.Security;

namespace StoreHouse360.Authorization
{
    public class PoliciesAuthorizationHandler : AuthorizationHandler<PoliciesAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PoliciesAuthorizationRequirement requirement)
        {
            var userPermissions = context.User.FindFirst(c => c.Type == AuthorizationClaimTypes.Permissions);

            if (userPermissions == null)
            {
                return Task.CompletedTask;
            }

            var requiredPermissions = requirement.Policies;

            var authorized = PolicyAuthorizer.Authorize(userPermissions.Value, requiredPermissions);

            if (!authorized) return Task.CompletedTask;

            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
