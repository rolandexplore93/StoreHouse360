using Microsoft.AspNetCore.Identity;
using StoreHouse360.Application.Services.Settings;
using StoreHouse360.Infrastructure.Persistence.Database.Models;
using StoreHouse360.Infrastructure.Persistence.Database.SeedData.Accounts;
using StoreHouse360.Infrastructure.Persistence.Database.SeedData.Currencies;
using StoreHouse360.Infrastructure.Persistence.Database.SeedData.UsersRoles;

namespace StoreHouse360.Infrastructure.Persistence.Database.SeedData
{
    public interface ISeedToDatabase : ISeedData
    {
    }

    public class SeedToDatabase : ISeedToDatabase
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly List<ISeedData> _seedData;
        public SeedToDatabase(RoleManager<AppRole> roleManager, UserManager<ApplicationIdentityUser> userManager)
        {
            _seedData = new List<ISeedData>
            {
                new AccountsSeeding(),
                new CurrenciesSeeding(),
                new UserRolesSeeding(roleManager, userManager)
            };
        }
        public Task Seed(ApplicationDbContext dbContext, IAppSettingsProvider settingsProvider)
        {
            return Task.WhenAll(_seedData.Select(seeder => seeder.Seed(dbContext, settingsProvider)));
        }
    }
}
