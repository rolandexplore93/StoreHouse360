using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Currencies
{
    public class GetAllCurrenciesQuery : IRequest<IEnumerable<Currency>>
    {

    }
    public class GetAllCurrenciesQueryHandler : IRequestHandler<GetAllCurrenciesQuery, IEnumerable<Currency>>
    {
        private readonly ICurrencyRepository _currencyRepository;
        public GetAllCurrenciesQueryHandler(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }
        public async Task<IEnumerable<Currency>> Handle(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
        {
            return await _currencyRepository.GetAllAsync();
        }
    }
}
