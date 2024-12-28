using StoreHouse360.Application.Commands.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Currencies
{
    public class UpdateCurrencyCommand : IUpdateEntityCommand<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public float Factor { get; set; }
    }

    public class UpdateCurrencyCommandHandler : UpdateEntityCommandHandler<UpdateCurrencyCommand, Currency, int, ICurrencyRepository>
    {
        public UpdateCurrencyCommandHandler(ICurrencyRepository repository) : base(repository)
        {
        }

        protected override Currency GetEntityToUpdate(UpdateCurrencyCommand request)
        {
            return new Currency { Id = request.Id, Name = request.Name, Symbol = request.Symbol, Factor = request.Factor };
        }
    }
}