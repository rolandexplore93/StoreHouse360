using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreHouse360.Application.Exceptions;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;
using StoreHouse360.Infrastructure.Extensions;
using StoreHouse360.Infrastructure.Persistence.Database.Models;

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
            return Task.CompletedTask;
        }
        public async Task<SaveAction<Task<User>>> CreateAsync(User user)
        {
            var identityUser = _mapper.Map<User, ApplicationIdentityUser>(user);
            var result = await _userManager.CreateAsync(identityUser, user.PasswordHash);
            if (result.Succeeded) return () => Task.FromResult(_mapper.Map<ApplicationIdentityUser, User>(identityUser));
            throw new Exception(result.GetErrorsAsString());
        }
        public async Task<IEnumerable<User>> GetAllAsync(GetAllOptions<User>? options = default)
        {
            return await _userManager.Users.ProjectTo<User>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<User> FindByIdAsync(int id, FindOptions? options = default)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) throw new NotFoundException("user", id);
            return _mapper.Map<User>(user);
        }

        public Task<User> FindIncludedByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) throw new NotFoundException();
            await _userManager.DeleteAsync(user);
        }

        public async Task<User> UpdateAsync(User user)
        {
            var model = await _userManager.FindByIdAsync(user.Id.ToString());
            if (model == null) throw new NotFoundException("user", user.Id);

            model.UserName = user.UserName;
            var result = await _userManager.UpdateAsync(model);
            if (!result.Succeeded)
            {
                throw new Exception(result.GetErrorsAsString());
            }
            return _mapper.Map<User>(model);
        }
    }
}
