using StoreHouse360.Application.Services.Settings;
using StoreHouse360.Infrastructure.Persistence.Database.Models;
using System.Configuration;

namespace StoreHouse360.Infrastructure.Persistence.Database.SeedData.Accounts
{
    public class MainPurchasesReturnsSeeder : ISeedData
    {
        public Task Seed(ApplicationDbContext dbContext, IAppSettingsProvider settingsProvider)
        {
            var settings = settingsProvider.Get();

            var mainPurchasesReturns = dbContext.Accounts
                .FirstOrDefault(account => account.Id == settings.DefaultPurchasesReturnsAccountId);

            if (mainPurchasesReturns != null)
            {
                return Task.CompletedTask;
            }

            using (var transaction = dbContext.Database.BeginTransaction())
            {
                var entry = dbContext.Accounts.Add(new AccountDb
                {
                    Name = "Main Purchases Returns",
                    Code = "MPRe",
                    City = "",
                    Phone = ""
                });

                dbContext.SaveChanges();

                settings.DefaultPurchasesReturnsAccountId = entry.Entity.Id;

                settingsProvider.Configure(settings);

                transaction.Commit();
            }

            return Task.CompletedTask;
        }
    }
}
