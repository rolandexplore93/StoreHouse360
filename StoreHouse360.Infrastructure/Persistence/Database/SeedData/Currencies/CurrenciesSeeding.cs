using StoreHouse360.Application.Services.Settings;

namespace StoreHouse360.Infrastructure.Persistence.Database.SeedData.Currencies
{
    public class CurrenciesSeeding : ISeedData
    {
        private readonly List<ISeedData> _seeders;
        public CurrenciesSeeding()
        {
            _seeders = new List<ISeedData>
        {
            new MainCurrencySeeder()
        };
        }

        public void Seed(ApplicationDbContext dbContext, IAppSettingsProvider settingsProvider)
        {
            _seeders.ForEach(seeder => seeder.Seed(dbContext, settingsProvider));
        }
    }
}
