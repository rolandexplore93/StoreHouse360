using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Authorization;
using StoreHouse360.Infrastructure.Persistence.Database.Models;
using System.Security.Claims;

namespace StoreHouse360.Infrastructure.Services
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationIdentityUser, AppRole>
    {
        public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationIdentityUser> userManager, RoleManager<AppRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationIdentityUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            var userRoleNames = await UserManager.GetRolesAsync(user);
            var userRoles = await RoleManager.Roles.Where(r => userRoleNames.Contains(r.Name)).ToListAsync();
            var userPermissions = new Permissions();

            foreach (var role in userRoles)
            {
                userPermissions.Merge(Permissions.From(role.Permissions));
            }

            var permissionsString = userPermissions.ToString();

            identity.AddClaim(new Claim(AuthorizationClaimTypes.Permissions, permissionsString));

            return identity;
        }
    }
}
