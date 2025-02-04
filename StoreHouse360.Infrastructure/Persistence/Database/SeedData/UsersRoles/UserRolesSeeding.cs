using Microsoft.AspNetCore.Identity;
using StoreHouse360.Application.Services.Settings;
using StoreHouse360.Infrastructure.Persistence.Database.Models;

namespace StoreHouse360.Infrastructure.Persistence.Database.SeedData.UsersRoles
{
    public class UserRolesSeeding : ISeedData
    {
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        private const string AdminRole = "admin";
        private const string AdminUsername = "admin-user";
        private const string AdminPassword = "admin-pass-123";
        private const string AdminPermissions = "all";

        private const string TempRole = "temp";
        private const string TempUsername = "temp-user";
        private const string TempPassword = "temp-pass-123";
        private const string TempPermissions = "users-read,users-write";

        public UserRolesSeeding(RoleManager<AppRole> roleManager, UserManager<ApplicationIdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task Seed(ApplicationDbContext dbContext, IAppSettingsProvider settingsProvider)
        {
            await _userManager.CreateAsync(
                new ApplicationIdentityUser
                {
                    UserName = AdminUsername
                }    
            );

            await _roleManager.CreateAsync(
                new AppRole
                {
                    Name = AdminRole,
                    NormalizedName = AdminRole.ToUpper(),
                    Permissions = AdminPermissions
                }
            );

            // check if admin exists
            var adminUser = await _userManager.FindByNameAsync( AdminUsername );
            if ( adminUser != null )
                // Assign role to admin
                await _userManager.AddToRoleAsync(adminUser, AdminRole);

            // Create Temporal User
            await _userManager.CreateAsync(
                new ApplicationIdentityUser
                {
                    UserName = TempUsername,
                },
                TempPassword
            );

            await _roleManager.CreateAsync(
                new AppRole
                {
                    Name = TempRole,
                    NormalizedName = TempRole.ToUpper(),
                    Permissions = TempPermissions
                }
            );

            var tempUser = await _userManager.FindByNameAsync(TempUsername);
            if ( tempUser != null ) 
                await _userManager.AddToRoleAsync(tempUser, TempRole);
        }
    }
}
