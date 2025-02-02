using StoreHouse360.Application.Services.Settings;

namespace StoreHouse360.Infrastructure.Persistence.Database.SeedData
{
    public interface ISeedData
    {
        public Task Seed(ApplicationDbContext dbContext, IAppSettingsProvider settingsProvider);
    }
}
