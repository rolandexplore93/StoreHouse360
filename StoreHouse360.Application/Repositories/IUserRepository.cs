using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Repositories
{
    public interface IUserRepository : IRepositoryCrud<User, int>
    {
        Task<User> UpdateAsync(User user);
    }
}
