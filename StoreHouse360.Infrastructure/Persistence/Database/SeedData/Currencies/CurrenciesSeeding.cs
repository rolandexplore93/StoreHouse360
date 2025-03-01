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

        public Task Seed(ApplicationDbContext dbContext, IAppSettingsProvider settingsProvider)
        {
            return Task.WhenAll(_seeders.Select(seeder => seeder.Seed(dbContext, settingsProvider)));
        }
    }
}
