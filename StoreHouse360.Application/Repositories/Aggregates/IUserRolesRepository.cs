using StoreHouse360.Application.Common.Security;

namespace StoreHouse360.Application.Repositories.Aggregates
{
    public interface IUserRolesRepository
    {
        Task<UserRoles> FindByUserId(int userId);
        Task<UserRoles> Update(UserRoles userRoles);
        IQueryable<UserRoles> GetAll();
    }
}
