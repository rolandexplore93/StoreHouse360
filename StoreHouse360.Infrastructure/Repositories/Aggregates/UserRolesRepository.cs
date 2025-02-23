using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Exceptions;
using StoreHouse360.Application.Repositories.Aggregates;
using StoreHouse360.Domain.Entities;
using StoreHouse360.Infrastructure.Persistence.Database;
using StoreHouse360.Infrastructure.Persistence.Database.Models;

namespace StoreHouse360.Infrastructure.Repositories.Aggregates
{
    public class UserRolesRepository : IUserRolesRepository
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;

        public UserRolesRepository(UserManager<ApplicationIdentityUser> userManager, IMapper mapper, RoleManager<AppRole> roleManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public async Task<UserRoles> FindByUserId(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                throw new NotFoundException();
            }

            var userEntity = _mapper.Map<ApplicationIdentityUser, User>(user);

            var rolesNames = await _userManager.GetRolesAsync(user);

            if (rolesNames.IsNullOrEmpty())
            {
                return new UserRoles(userEntity, new List<Role>());
            }

            var roles = _roleManager.Roles.Where(r => rolesNames.Contains(r.Name))
                .ProjectTo<Role>(_mapper.ConfigurationProvider);

            return new UserRoles(userEntity, roles.ToList());
        }

        public async Task<UserRoles> Update(UserRoles userRoles)
        {
            var user = await _userManager.FindByIdAsync(userRoles.User.Id.ToString());

            if (user == null)
            {
                throw new NotFoundException();
            }

            var currentUserRoles = await _userManager.GetRolesAsync(user);

            if (!currentUserRoles.IsNullOrEmpty())
            {
                await _userManager.RemoveFromRolesAsync(user, currentUserRoles);
            }

            await _userManager.AddToRolesAsync(user, userRoles.RoleStrings);

            return await FindByUserId(user.Id);
        }

        public IQueryable<UserRoles> GetAll()
        {
            var result = _dbContext.UserRoles
                .Join(_dbContext.Users, userRole => userRole.UserId, user => user.Id, (userRole, user) =>
                    new
                    {
                        user.Id,
                        user.UserName,
                        userRole.RoleId
                    })
                .Join(_dbContext.Roles, user => user.RoleId, role => role.Id,
                    (user, role) => new
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        RoleId = role.Id,
                        RoleName = role.Name,
                        Permissions = role.Permissions,
                    })
                .GroupBy(
                    key => new { Id = key.UserId, key.UserName },
                    value => new { Id = value.RoleId, Name = value.RoleName, value.Permissions },
                    (user, roles) =>
                        new
                        {
                            user,
                            roles = roles.ToList()
                        }
                )
                .ToList()
                .Select(
                    userRole => new UserRoles(
                        new User { Id = userRole.user.Id, UserName = userRole.user.UserName },
                        userRole.roles.Select(r => new Role(r.Id, r.Name, Permissions.From(r.Permissions))).ToList()
                    )
                )
                .AsQueryable();

            return result;
        }
    }
}
