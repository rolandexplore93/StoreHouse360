using StoreHouse360.Application.Settings;

namespace StoreHouse360.Application.Services.Settings
{
    public interface IAppSettingsProvider
    {
        public AppSettings Get();
        public void Configure(AppSettings settings);
    }
}
