using AutoMapper;
using StoreHouse360.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using StoreHouse360.Application.Exceptions;
using StoreHouse360.Infrastructure.Models;
using StoreHouse360.Application.Services.Identity;

namespace StoreHouse360.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        public IdentityService(IMapper mapper, UserManager<ApplicationIdentityUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            var identityUser = await _userManager.FindByIdAsync(user.Id.ToString());

            return await _userManager.CheckPasswordAsync(identityUser, password);
        }
        public async Task<User> FindUserByNameAsync(string username)
        {
            var identityUser = await _userManager.FindByNameAsync(username);

            if (identityUser == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<ApplicationIdentityUser, User>(identityUser);
        }
    }
}