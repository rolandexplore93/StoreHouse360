using Microsoft.Extensions.DependencyInjection;
using StoreHouse360.Application.Common.Mappings;
using System.Reflection;

namespace StoreHouse360.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }

        public static void AddApplicationAutomapper(this IServiceCollection services, Assembly[] assemblies)
        {
            services.AddAutoMapper(config => config.AddProfile(new MappingProfiles(assemblies)));
        }
    }
}
