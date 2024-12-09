using StoreHouse360.Application.Commands.Currencies;
using StoreHouse360.Application.Common.Mappings;

namespace StoreHouse360.Dto.Currencies
{
    public class CreateCurrencyRequestDTO : IMapFrom<CreateCurrencyCommand>
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public float Factor { get; set; }
    }
}
