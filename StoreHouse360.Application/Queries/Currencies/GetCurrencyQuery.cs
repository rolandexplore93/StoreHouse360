﻿using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Currencies
{

    public class GetCurrencyQuery : IRequest<Currency>
    {
        public int Id { get; set; }
    }
    public class GetCurrencyQueryHandler : IRequestHandler<GetCurrencyQuery, Currency>
    {
        private readonly ICurrencyRepository _currencyRepository;
        public GetCurrencyQueryHandler(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }
        public Task<Currency> Handle(GetCurrencyQuery request, CancellationToken cancellationToken)
        {
            return _currencyRepository.FindByIdAsync(request.Id);
        }
    }
}
