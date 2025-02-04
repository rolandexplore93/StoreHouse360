using StoreHouse360.Application.Services.Settings;
using StoreHouse360.Domain.Entities;
using StoreHouse360.Infrastructure.Persistence.Database.Models;

namespace StoreHouse360.Infrastructure.Persistence.Database.SeedData.Accounts
{
    public class MainCashDrawerSeeder : ISeedData
    {
        public Task Seed(ApplicationDbContext dbContext, IAppSettingsProvider settingsProvider)
        {
            var settings = settingsProvider.Get();

            var mainCashDrawer = dbContext.Accounts.FirstOrDefault(acct => acct.Id == settings.DefaultMainCashDrawerAccountId);

            if (mainCashDrawer != null)
            {
                return Task.CompletedTask;
            }

            using (var transaction = dbContext.Database.BeginTransaction())
            {
                var entry = dbContext.Accounts.Add(new AccountDb
                {
                    Name = "Main Cash Drawer",
                    Code = "MCD",
                    City = "",
                    Phone = ""
                });

                dbContext.SaveChanges();
                settings.DefaultMainCashDrawerAccountId = entry.Entity.Id;
                settingsProvider.Configure(settings);
                transaction.Commit();
            }

            return Task.CompletedTask;
        }
    }
}
