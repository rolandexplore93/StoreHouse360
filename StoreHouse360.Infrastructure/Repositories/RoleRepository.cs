using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Exceptions;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Infrastructure.Extensions;
using StoreHouse360.Infrastructure.Persistence.Database.Models;

namespace StoreHouse360.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IMapper _mapper;
        private readonly RoleManager<AppRole> _roleManager;

        public RoleRepository(RoleManager<AppRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<SaveAction<Task<Role>>> CreateAsync(Role role)
        {
            var applicationRole = _mapper.Map<Role, AppRole>(role);

            var result = await _roleManager.CreateAsync(applicationRole);

            if (result.Succeeded)
                return () => Task.FromResult(_mapper.Map<AppRole, Role>(applicationRole));

            throw new Exception(result.GetErrorsAsString());
        }

        public async Task<Role> Update(Role role)
        {
            var applicationRole = await _roleManager.FindByIdAsync(role.Id.ToString());

            if (applicationRole == null)
            {
                throw new NotFoundException();
            }

            applicationRole.Permissions = role.Permissions.ToString();
            applicationRole.Name = role.Name;

            var result = await _roleManager.UpdateAsync(applicationRole);

            if (result.Succeeded)
                return _mapper.Map<AppRole, Role>(applicationRole);

            throw new Exception(result.GetErrorsAsString());
        }

        public async Task<Role> FindByIdAsync(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (role == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<AppRole, Role>(role);
        }

        public IQueryable<Role> GetAll()
        {
            return _roleManager.Roles.ProjectTo<Role>(_mapper.ConfigurationProvider);
        }

        public async Task DeleteAsync(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (role == null)
            {
                throw new NotFoundException();
            }

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
                return;

            throw new Exception(result.GetErrorsAsString());
        }
    }
}
