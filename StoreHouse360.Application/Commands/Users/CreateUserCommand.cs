﻿using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Users
{
    public class CreateUserCommand : IRequest<int>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User() { UserName = request.Username, PasswordHash = request.Password };
            var createdUser = await _userRepository.CreateAsync(user);
            await _userRepository.SaveChanges();
            return createdUser.Id;
        }
    }
}
