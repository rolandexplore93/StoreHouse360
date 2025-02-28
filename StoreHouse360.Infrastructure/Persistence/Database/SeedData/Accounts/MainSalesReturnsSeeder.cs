using StoreHouse360.Application.Services.Settings;
using StoreHouse360.Infrastructure.Persistence.Database.Models;
using System.Configuration;

namespace StoreHouse360.Infrastructure.Persistence.Database.SeedData.Accounts
{
    public class MainSalesReturnsSeeder : ISeedData
    {
        public Task Seed(ApplicationDbContext dbContext, IAppSettingsProvider settingsProvider)
        {
            var settings = settingsProvider.Get();

            var mainSalesReturns = dbContext.Accounts
                .FirstOrDefault(account => account.Id == settings.DefaultSalesReturnsAccountId);

            if (mainSalesReturns != null)
            {
                return Task.CompletedTask;
            }

            using (var transaction = dbContext.Database.BeginTransaction())
            {
                var entry = dbContext.Accounts.Add(new AccountDb
                {
                    Name = "Main Sales Return",
                    Code = "MSRe",
                    City = "",
                    Phone = ""
                });

                dbContext.SaveChanges();

                settings.DefaultSalesReturnsAccountId = entry.Entity.Id;

                settingsProvider.Configure(settings);

                transaction.Commit();
            }

            return Task.CompletedTask;
        }
    }
}
