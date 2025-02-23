using Microsoft.AspNetCore.Authorization;

namespace StoreHouse360.Authorization
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppAuthorization(this IServiceCollection services)
        {
            return services
                .AddSingleton<IAuthorizationHandler, PoliciesAuthorizationHandler>()
                .AddSingleton<IAuthorizationPolicyProvider, AppAuthorizationPolicyProvider>();
        }
    }
}