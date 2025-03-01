using StoreHouse360.Application.Services.Settings;
using StoreHouse360.Infrastructure.Persistence.Database.Models;

namespace StoreHouse360.Infrastructure.Persistence.Database.SeedData.Accounts
{
    public class MainImportsSeeder : ISeedData
    {
        public Task Seed(ApplicationDbContext dbContext, IAppSettingsProvider settingsProvider)
        {
            var settings = settingsProvider.Get();

            var mainImports = dbContext.Accounts.FirstOrDefault(account => account.Id == settings.DefaultMainImportsAccountId);

            if (mainImports != null) return Task.CompletedTask;

            using (var transaction = dbContext.Database.BeginTransaction())
            {
                var entry = dbContext.Accounts.Add(new AccountDb
                {
                    Name = "Main Imports",
                    Code = "MIm",
                    City = "",
                    Phone = ""
                });

                dbContext.SaveChanges();
                settings.DefaultMainImportsAccountId = entry.Entity.Id;
                settingsProvider.Configure(settings);
                transaction.Commit();
            }

            return Task.CompletedTask;
        }
    }
}
