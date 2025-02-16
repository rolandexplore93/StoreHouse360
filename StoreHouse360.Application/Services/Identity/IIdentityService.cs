using StoreHouse360.Application.Common.Security;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Services.Identity
{
    public interface IIdentityService
    {
        Task<bool> CheckPasswordAsync(User user, string password);

        /// <exception cref="NotFoundException"></exception>
        /// <param name="username"></param>
        /// <returns>The matched user</returns>
        Task<User> FindUserByNameAsync(string username);
        Task<bool> AuthorizeAsync(int userId, string policyName);
        Task<bool> AuthorizeAsync(int userId, IList<Policy> policies);
    }
}
