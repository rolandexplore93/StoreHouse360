using StoreHouse360.Application.Services.Settings;
using StoreHouse360.Infrastructure.Persistence.Database.Models;

namespace StoreHouse360.Infrastructure.Persistence.Database.SeedData.Currencies
{
    public class MainCurrencySeeder : ISeedData
    {
        public void Seed(ApplicationDbContext dbContext, IAppSettingsProvider settingsProvider)
        {
            var settings = settingsProvider.Get();
            var mainCurrency = dbContext.Currencies.FirstOrDefault(account => account.Id == settings.DefaultCurrencyId);

            if (mainCurrency != null)
            {
                return;
            }

            using (var transaction = dbContext.Database.BeginTransaction())
            {
                var entry = dbContext.Currencies.Add(new CurrencyDb()
                {
                    Name = "Main Sales",
                    Symbol = "SYP",
                    Factor = 1
                });
                dbContext.SaveChanges();
                settings.DefaultCurrencyId = entry.Entity.Id;

                settingsProvider.Configure(settings);

                transaction.Commit();
            }
        }
    }
}
