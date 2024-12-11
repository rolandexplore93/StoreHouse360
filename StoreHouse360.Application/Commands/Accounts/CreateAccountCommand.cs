﻿using StoreHouse360.Application.Commands.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Accounts
{
    public class CreateAccountCommand : ICreateEntityCommand<int>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
    }

    public class CreateAccountCommandHandler : CreateEntityCommandHandler<CreateAccountCommand, Account, int, IAccountRepository>
    {
        public CreateAccountCommandHandler(IAccountRepository repository) : base(repository)
        {
        }
        protected override Account CreateEntity(CreateAccountCommand request) => new()
        {
            Name = request.Name,
            Code = request.Code,
            Phone = request.Phone,
            City = request.City
        };
    }
}
