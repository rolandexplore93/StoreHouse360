using MediatR;
using StoreHouse360.Application.Common.QueryFilters;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Accounts
{
    [Authorize(Method = Method.Read, Resource = Resource.Accounts)]
    public class GetAllAccountsQuery : GetPaginatedQuery<Account>
    {
        [QueryFilter(QueryFilterCompareType.StringContains)]
        public string? Name { get; set; }
    }
    public class GetAllAccountsQueryHandler : PaginatedQueryHandler<GetAllAccountsQuery, Account>
    {
        private readonly IAccountRepository _repository;
        public GetAllAccountsQueryHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        protected override async Task<IQueryable<Account>> GetQuery(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}