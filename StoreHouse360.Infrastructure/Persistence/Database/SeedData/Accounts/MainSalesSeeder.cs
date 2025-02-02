using StoreHouse360.Application.Services.Settings;
using StoreHouse360.Domain.Entities;
using StoreHouse360.Infrastructure.Persistence.Database.Models;

namespace StoreHouse360.Infrastructure.Persistence.Database.SeedData.Accounts
{
    public class MainSalesSeeder : ISeedData
    {
        public Task Seed(ApplicationDbContext dbContext, IAppSettingsProvider settingsProvider)
        {
            var settings = settingsProvider.Get();

            var mainSales = dbContext.Accounts.FirstOrDefault(acct => acct.Id == settings.DefaultSalesAccountId);

            if (mainSales != null)
            {
                return Task.CompletedTask;
            }

            using (var transaction = dbContext.Database.BeginTransaction())
            {
                var entry = dbContext.Accounts.Add(new AccountDb
                {
                    Name = "Main Sales",
                    Code = "MSa",
                    City = "",
                    Phone = ""
                });

                dbContext.SaveChanges();
                settings.DefaultSalesAccountId = entry.Entity.Id;
                settingsProvider.Configure(settings);
                transaction.Commit();
            }

            return Task.CompletedTask;
        }
    }
}
