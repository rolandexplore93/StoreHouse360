using MediatR;
using StoreHouse360.Application.Common.QueryFilters;
using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Currencies
{
    public class GetAllCurrenciesQuery : GetPaginatedQuery<Currency>
    {
        [QueryFilter(QueryFilterCompareType.StringContains)]
        public string? Name { get; set; }
    }
    public class GetAllCurrenciesQueryHandler : PaginatedQueryHandler<GetAllCurrenciesQuery, Currency>
    {
        private readonly ICurrencyRepository _currencyRepository;
        public GetAllCurrenciesQueryHandler(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        protected override async Task<IQueryable<Currency>> GetQuery(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
        {
            return await _currencyRepository.GetAllAsync();
        }
    }
}
