﻿using StoreHouse360.Application.Commands.Common;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Currencies
{
    [Authorize(Method = Method.Write, Resource = Resource.Currencies)]
    public class CreateCurrencyCommand : ICreateEntityCommand<int>
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public float Factor { get; set; }
    }

    public class CreateCurrencyCommandHandler : CreateEntityCommandHandler<CreateCurrencyCommand, Currency, int, ICurrencyRepository>
    {
        public CreateCurrencyCommandHandler(ICurrencyRepository repository) : base(repository)
        {
        }

        protected override Currency CreateEntity(CreateCurrencyCommand request)
        {
            return new Currency { Name = request.Name, Symbol = request.Symbol, Factor = request.Factor };
        }
    }
}