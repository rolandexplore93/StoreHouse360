using MediatR;
using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Accounts
{
    public class GetAllAccountsQuery : GetPaginatedQuery<Account>
    {
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