using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Infrastructure.Models;
using StoreHouse360.Infrastructure.Persistence.Database;
using StoreHouse360.Infrastructure.Repositories;
using StoreHouse360.Infrastructure.Services;

namespace StoreHouse360.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
                services.AddDbContext<ApplicationDbContext>(options => { options.UseInMemoryDatabase("sh360_db"); });
            else
                services.AddSqlServer<ApplicationDbContext>(configuration.GetConnectionString("DefaultConnection"));

            services.AddUserIdentityServer();
            services.AddRepositories();
            services.AddServices();

            return services;
        }

        static void AddUserIdentityServer(this IServiceCollection services)
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

        public static void AddRepositories (this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }

        static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IdentityService, IdentityService>();
        }
    }
}
