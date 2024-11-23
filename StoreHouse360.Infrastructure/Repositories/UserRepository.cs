using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;
using StoreHouse360.Infrastructure.Extensions;
using StoreHouse360.Infrastructure.Models;

namespace StoreHouse360.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly IMapper _mapper;
        public UserRepository(UserManager<ApplicationIdentityUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }
        public async Task<User> CreateAsync(User user)
        {
            var identityUser = _mapper.Map<User, ApplicationIdentityUser>(user);
            var result = await _userManager.CreateAsync(identityUser, user.PasswordHash);
            if (result.Succeeded)
                return _mapper.Map<ApplicationIdentityUser, User>(identityUser);
            throw new Exception(result.GetErrorsAsString());
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userManager.Users.ProjectTo<User>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public Task<User> FindByIdAsync(int id)
        {
            return _userManager.FindByIdAsync(id.ToString()).ContinueWith(task => _mapper.Map<User>(task.Result));
        }
    }
}
