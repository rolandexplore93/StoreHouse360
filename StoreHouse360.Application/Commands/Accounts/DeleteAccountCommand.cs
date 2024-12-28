using MediatR;
using StoreHouse360.Application.Commands.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Accounts
{
    public class DeleteAccountCommand : DeleteEntityCommand<int>
    {
    }
    public class DeleteAccountCommandHandler : DeleteEntityCommandHandler<DeleteAccountCommand, Account, int, IAccountRepository>
    {
        public DeleteAccountCommandHandler(IAccountRepository repository) : base(repository)
        {
        }
    }
}
