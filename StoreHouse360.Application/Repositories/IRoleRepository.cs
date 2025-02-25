﻿
using StoreHouse360.Application.Common.Security;

namespace StoreHouse360.Application.Repositories
{
    public interface IRoleRepository
    {
        Task<SaveAction<Task<Role>>> CreateAsync(Role role);
        Task<Role> Update(Role role);

        Task<Role> FindByIdAsync(int id);

        IQueryable<Role> GetAll();

        Task DeleteAsync(int id);
    }
}
