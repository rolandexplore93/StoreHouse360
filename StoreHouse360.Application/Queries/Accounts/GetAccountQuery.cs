using MediatR;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Accounts
{
    [Authorize(Method = Method.Read, Resource = Resource.Accounts)]
    public class GetAccountQuery : IRequest<Account>
    {
        public int Id { get; set; }
    }
    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, Account>
    {
        private readonly IAccountRepository _repository;
        public GetAccountQueryHandler(IAccountRepository repository)
        {
            _repository = repository;
        }
        public async Task<Account> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            return await _repository.FindByIdAsync(request.Id);
        }
    }
}
