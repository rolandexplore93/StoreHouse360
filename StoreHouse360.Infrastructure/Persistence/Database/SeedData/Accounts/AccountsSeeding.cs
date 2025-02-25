using StoreHouse360.Application.Services.Settings;

namespace StoreHouse360.Infrastructure.Persistence.Database.SeedData.Accounts
{
    public class AccountsSeeding : ISeedData
    {
        private readonly List<ISeedData> _seedData;
        public AccountsSeeding()
        {
            _seedData = new List<ISeedData>
            {
                new MainCashDrawerSeeder(),
                new MainPurchasesSeeder(),
                new MainSalesSeeder(),
                new MainImportsSeeder(),
                new MainExportsSeeder(),
                new ConversionsSeeder(),
            };
        }
        public Task Seed(ApplicationDbContext dbContext, IAppSettingsProvider settingsProvider)
        {
            return Task.WhenAll(_seedData.Select(seeder => seeder.Seed(dbContext, settingsProvider)));
        }
    }
}