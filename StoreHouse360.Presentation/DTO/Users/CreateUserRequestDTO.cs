﻿using StoreHouse360.Application.Commands.Users;
using StoreHouse360.Application.Common.Mappings;

namespace StoreHouse360.DTO.Users
{
    public class CreateUserRequestDTO : IMapFrom<CreateUserCommand>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
