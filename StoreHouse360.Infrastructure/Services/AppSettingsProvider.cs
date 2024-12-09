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

        private readonly ApplicationDbContext _context;
        public AppSettingsProvider(ApplicationDbContext context)
        {
            _context = context;
        }

        public AppSettings Get()
        {

            var data = _context.Settings.ToDictionary(setting => setting.Key, setting => setting.Value);
            return JsonConvert.DeserializeObject<AppSettings>(JsonConvert.SerializeObject(data))!;
        }


        public void Configure(AppSettings settings)
        {
            foreach (var property in typeof(AppSettings).GetProperties())
            {
                var settingAlreadyCreated = _context.Settings.Any(setting => setting.Key.Equals(property));
                AppSettingDb settingDb = new AppSettingDb
                {
                    Key = property.Name,
                    Value = property.GetValue(settings)!.ToString()!,
                };
                _context.Entry(settingDb).State = settingAlreadyCreated ? EntityState.Modified : EntityState.Added;
                _context.SaveChanges();
            }
        }
    }
}
