using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreHouse360.Application.Exceptions;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;
using StoreHouse360.Infrastructure.Extensions;
using StoreHouse360.Infrastructure.Persistence.Database.Models;
using System.Collections.Generic;

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

            if (result.Succeeded) 
                return () => Task.FromResult(_mapper.Map<ApplicationIdentityUser, User>(identityUser));

            throw new Exception(result.GetErrorsAsString());
        }
        public async Task<IQueryable<User>> GetAllAsync(GetAllOptions<User>? options = default)
        {
            return _userManager.Users.ProjectTo<User>(_mapper.ConfigurationProvider);
        }

        public async Task<User> FindByIdAsync(int id, FindOptions? options = default)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) 
                throw new NotFoundException("user", id);
            return _mapper.Map<User>(user);
        }

        public Task<User> FindIncludedByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistsById(int id)
        {
            return _userManager.Users.AnyAsync(user => user.Id == id);
        }

        public async Task<User> UpdateAsync(User user)
        {
            var model = await _userManager.FindByIdAsync(user.Id.ToString());
            if (model == null) 
                throw new NotFoundException("user", user.Id);

            model.UserName = user.UserName;

            if (!string.IsNullOrEmpty(user.PasswordHash))
                model.PasswordHash = _userManager.PasswordHasher.HashPassword(model, user.PasswordHash);

            var result = await _userManager.UpdateAsync(model);

            if (!result.Succeeded)
            {
                throw new Exception(result.GetErrorsAsString());
            }

            return _mapper.Map<User>(model);
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) 
                throw new NotFoundException();
            await _userManager.DeleteAsync(user);
        }

        public Task<SaveAction<Task<IEnumerable<User>>>> CreateAllAsync(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }
    }
}
