using StoreHouse360.Application.Services.Settings;
using StoreHouse360.Infrastructure.Persistence.Database.Models;

namespace StoreHouse360.Infrastructure.Persistence.Database.SeedData.Accounts
{
    public class MainExportsSeeder : ISeedData
    {
        public Task Seed(ApplicationDbContext dbContext, IAppSettingsProvider settingsProvider)
        {
            var settings = settingsProvider.Get();

            var mainExports = dbContext.Accounts.FirstOrDefault(account => account.Id == settings.DefaultMainExportsAccountId);

            if (mainExports != null) return Task.CompletedTask;

            using (var transaction = dbContext.Database.BeginTransaction())
            {
                var entry = dbContext.Accounts.Add(new AccountDb
                {
                    Name = "Main Exports",
                    Code = "MEx",
                    City = "",
                    Phone = ""
                });

                dbContext.SaveChanges();
                settings.DefaultMainExportsAccountId = entry.Entity.Id;
                settingsProvider.Configure(settings);
                transaction.Commit();
            }

            return Task.CompletedTask;
        }
    }
}
