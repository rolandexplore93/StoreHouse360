using StoreHouse360.Application.Services.Settings;
using StoreHouse360.Infrastructure.Persistence.Database.SeedData.Accounts;
using StoreHouse360.Infrastructure.Persistence.Database.SeedData.Currencies;

namespace StoreHouse360.Infrastructure.Persistence.Database.SeedData
{
    public interface ISeedToDatabase : ISeedData
    {
    }

    public class SeedToDatabase : ISeedToDatabase
    {
        private readonly List<ISeedData> _seedData;
        public SeedToDatabase()
        {
            _seedData = new List<ISeedData>
            {
                new AccountsSeeding(),
                new CurrenciesSeeding()
            };
        }
        public void Seed(ApplicationDbContext dbContext, IAppSettingsProvider settingsProvider)
        {
            _seedData.ForEach(seeder => seeder.Seed(dbContext, settingsProvider));
            dbContext.SaveChanges();
        }
    }
}
