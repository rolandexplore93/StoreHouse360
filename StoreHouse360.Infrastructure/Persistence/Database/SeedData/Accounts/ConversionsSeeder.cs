using StoreHouse360.Application.Services.Settings;
using StoreHouse360.Infrastructure.Persistence.Database.Models;

namespace StoreHouse360.Infrastructure.Persistence.Database.SeedData.Accounts
{
    public class ConversionsSeeder : ISeedData
    {

        public Task Seed(ApplicationDbContext dbContext, IAppSettingsProvider settingsProvider)
        {
            var settings = settingsProvider.Get();

            var conversionsAccount = dbContext.Accounts.FirstOrDefault(account => account.Id == settings.DefaultConversionsAccountId);

            if (conversionsAccount != null) return Task.CompletedTask;

            using (var transaction = dbContext.Database.BeginTransaction())
            {
                var entry = dbContext.Accounts.Add(new AccountDb
                {
                    Name = "RolandProwess",
                    Code = "Co",
                    City = "",
                    Phone = ""
                });

                dbContext.SaveChanges();
                settings.DefaultConversionsAccountId = entry.Entity.Id;
                settingsProvider.Configure(settings);
                transaction.Commit();
            }

            return Task.CompletedTask;
        }
    }
}
