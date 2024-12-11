using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Accounts
{
    public class GetAllAccountsQuery : IRequest<IEnumerable<Account>>
    {
    }
    public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, IEnumerable<Account>>
    {
        private readonly IAccountRepository _repository;
        public GetAllAccountsQueryHandler(IAccountRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Account>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}