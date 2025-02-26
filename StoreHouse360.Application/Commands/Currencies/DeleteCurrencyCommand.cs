using StoreHouse360.Application.Commands.Common;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Currencies
{
    [Authorize(Method = Method.Delete, Resource = Resource.Currencies)]
    public class DeleteCurrencyCommand : DeleteEntityCommand<int>
    {
    }
    public class DeleteCurrencyCommandHandler : DeleteEntityCommandHandler<DeleteCurrencyCommand, Currency, int, ICurrencyRepository>
    {
        public DeleteCurrencyCommandHandler(ICurrencyRepository repository) : base(repository)
        {
        }
    }
}
