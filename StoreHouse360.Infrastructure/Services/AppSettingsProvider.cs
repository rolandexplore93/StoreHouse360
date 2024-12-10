using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StoreHouse360.Application.Services.Settings;
using StoreHouse360.Application.Settings;
using StoreHouse360.Infrastructure.Persistence.Database;
using StoreHouse360.Infrastructure.Persistence.Database.Models;

namespace StoreHouse360.Infrastructure.Services
{
    public class AppSettingsProvider : IAppSettingsProvider
    {

        private readonly ApplicationDbContext _context; // Database context to access the settings table

        // Creates the instance of AppSettingsProvider and inject ApplicationDbContext dependency via DI
        public AppSettingsProvider(ApplicationDbContext context)
        {
            _context = context;
        }

        public AppSettings Get()
        {
            // Get all settings from the database as key-value pairs.
            var data = _context.Settings.ToDictionary(setting => setting.Key, setting => setting.Value);

            // First, convert the Dictionary to JSON and then Deserialize it into an 'AppSettings' object
            return JsonConvert.DeserializeObject<AppSettings>(JsonConvert.SerializeObject(data))!;
        }

        public void Configure(AppSettings settings)
        {
            // Iterates through each property in the 'AppSettings' class using reflection.
            var propertyValues = typeof(AppSettings).GetProperties();
            foreach (var property in propertyValues)
            {
                // Check if the setting already exists in the db
                var settingAlreadyCreated = _context.Settings.Any(setting => setting.Key.Equals(property));

                // Maps the property name and value to a new 'AppSettingDb' object.
                AppSettingDb settingDb = new AppSettingDb
                {
                    Key = property.Name,
                    Value = property.GetValue(settings)!.ToString()!
                };

                // Marks the entity as 'Modified' if it exists, or 'Added' if it doesn’t exist.
                _context.Entry(settingDb).State = settingAlreadyCreated ? EntityState.Modified : EntityState.Added;
                _context.SaveChanges();
            }
        }
    }
}
