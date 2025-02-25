using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using StoreHouse360.DTO.Common;

namespace StoreHouse360.DTO.Currencies
{
    public class CurrencyVM : IMapFrom<Currency>, IViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public float Factor { get; set; }
    }
}
