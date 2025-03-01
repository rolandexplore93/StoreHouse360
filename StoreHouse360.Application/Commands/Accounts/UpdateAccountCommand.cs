using StoreHouse360.Application.Commands.Common;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Accounts
{
    [Authorize(Method = Method.Update, Resource = Resource.Accounts)]
    public class UpdateAccountCommand : IUpdateEntityCommand<int>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
    }
    public class
        UpdateAccountCommandHandler : UpdateEntityCommandHandler<UpdateAccountCommand, Account, int, IAccountRepository>
    {
        public UpdateAccountCommandHandler(IAccountRepository repository) : base(repository)
        {
        }
        protected override Account GetEntityToUpdate(UpdateAccountCommand request) => new(
            id: default,
            name: request.Name,
            code: request.Code,
            phone: request.Phone,
            city: request.City
        );
    }
}
