using StoreHouse360.Application.Services.Settings;
using StoreHouse360.Domain.Entities;
using StoreHouse360.Infrastructure.Persistence.Database.Models;

namespace StoreHouse360.Infrastructure.Persistence.Database.SeedData.Accounts
{
    public class MainPurchasesSeeder : ISeedData
    {
        public void Seed(ApplicationDbContext dbContext, IAppSettingsProvider settingsProvider)
        {
            var settings = settingsProvider.Get();

            var mainPurchases = dbContext.Accounts.FirstOrDefault(acct => acct.Id == settings.DefaultPurchasesAccountId);

            if (mainPurchases != null)
            {
                return;
            }

            using (var transaction = dbContext.Database.BeginTransaction())
            {
                var entry = dbContext.Accounts.Add(new AccountDb
                {
                    Name = "Main Purchases",
                    Code = "MPu",
                    City = "",
                    Phone = ""
                });

                dbContext.SaveChanges();
                settings.DefaultPurchasesAccountId = entry.Entity.Id;
                settingsProvider.Configure(settings);
                transaction.Commit();
            }
        }
    }
}
