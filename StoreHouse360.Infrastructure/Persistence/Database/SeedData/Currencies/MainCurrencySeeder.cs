using StoreHouse360.Application.Services.Settings;
using StoreHouse360.Infrastructure.Persistence.Database.Models;

namespace StoreHouse360.Infrastructure.Persistence.Database.SeedData.Currencies
{
    public class MainCurrencySeeder : ISeedData
    {
        public Task Seed(ApplicationDbContext dbContext, IAppSettingsProvider settingsProvider)
        {
            var settings = settingsProvider.Get();
            var mainCurrency = dbContext.Currencies.FirstOrDefault(currency => currency.Id == settings.DefaultCurrencyId);

            if (mainCurrency != null)
            {
                return Task.CompletedTask;
            }

            using (var transaction = dbContext.Database.BeginTransaction())
            {
                var entry = dbContext.Currencies.Add(new CurrencyDb()
                {
                    Name = "Pound Sterling",
                    Symbol = "GBP",
                    Factor = 1
                });
                dbContext.SaveChanges();
                settings.DefaultCurrencyId = entry.Entity.Id;

                settingsProvider.Configure(settings);

                transaction.Commit();
            }

            return Task.CompletedTask;
        }
    }
}
