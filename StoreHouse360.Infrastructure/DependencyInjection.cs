﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Application.Services.Identity;
using StoreHouse360.Infrastructure.Persistence.Database;
using StoreHouse360.Infrastructure.Persistence.Database.Models;
using StoreHouse360.Infrastructure.Repositories;
using StoreHouse360.Infrastructure.Services;
using System.Reflection;

namespace StoreHouse360.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
                services.AddDbContext<ApplicationDbContext>(options => { options.UseInMemoryDatabase("sh360_db"); });
            else
                services.AddSqlServer<ApplicationDbContext>(configuration.GetConnectionString("DefaultConnection"));

            services.AddUserIdentityServer();
            services.AddRepositories();
            services.AddServices();

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
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IIdentityService, IdentityService>();
        }
    }
}
