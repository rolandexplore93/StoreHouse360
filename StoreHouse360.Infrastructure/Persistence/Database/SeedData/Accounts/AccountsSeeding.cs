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
                new MainSalesSeeder()
            };
        }
        public void Seed(ApplicationDbContext dbContext, IAppSettingsProvider settingsProvider)
        {
            _seedData.ForEach(seeder => seeder.Seed(dbContext, settingsProvider));
        }
    }
}