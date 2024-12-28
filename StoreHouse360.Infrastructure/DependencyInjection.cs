using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Application.Repositories.UnitOfWork;
using StoreHouse360.Application.Services.Identity;
using StoreHouse360.Application.Services.Settings;
using StoreHouse360.Application.Settings;
using StoreHouse360.Infrastructure.Persistence.Database;
using StoreHouse360.Infrastructure.Persistence.Database.Models;
using StoreHouse360.Infrastructure.Persistence.Database.Triggers;
using StoreHouse360.Infrastructure.Repositories;
using StoreHouse360.Infrastructure.Repositories.UnitOfWork;
using StoreHouse360.Infrastructure.Services;
using System.Reflection;

namespace StoreHouse360.Infrastructure
{
    public static class DependencyInjection
    {
        // Extension method to register infrastructure services in the DI container
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options => { options.UseInMemoryDatabase("sh360_db"); });
            }
            else
            {
                services.AddSqlServer<ApplicationDbContext>(configuration.GetValue<bool>("UseLocalDatabaseServer")
                    ? configuration.GetConnectionString("LocalConnection")
                    : configuration.GetConnectionString("DefaultConnection"),
                    optionsAction: options =>
                    {
                        options.UseTriggers(trigOptions => trigOptions.AddTrigger<SoftDeleteTrigger>());
                    });
            }

            services.AddUserIdentityServer();
            services.AddRepositories();
            services.AddServices();
            services.AddAppSettings();

            return services;
        }

        private static void AddUserIdentityServer(this IServiceCollection services)
        {
            services.AddDefaultIdentity<ApplicationIdentityUser>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }

        private static void AddRepositories (this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            services.AddScoped<IStoragePlaceRepository, StoragePlaceRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>(); 
            services.AddScoped<ICurrencyAmountRepository, CurrencyAmountRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IProductMovementRepository, ProductMovementRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IIdentityService, IdentityService>();
        }

        private static void AddAppSettings(this IServiceCollection services)
        {
            services.AddScoped<IAppSettingsProvider, AppSettingsProvider>();
            services.AddScoped<AppSettings>(setting => setting.GetService<IAppSettingsProvider>()!.Get());
        }

        public static void ApplyMigrationToDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using (var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    dbContext.Database.Migrate(); // Apply pending migrations to the database and create the database if it does not exist   
                    dbContext.Database.EnsureCreated(); // Create database or tables if they do not exist
                }
            }
        }
    }
}
