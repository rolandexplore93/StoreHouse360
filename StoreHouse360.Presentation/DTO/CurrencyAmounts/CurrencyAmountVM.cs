using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using StoreHouse360.DTO.Common;

namespace StoreHouse360.DTO.CurrencyAmounts
{
    public class CurrencyAmountVM : IViewModel, IMapFrom<CurrencyAmount>
    {
        public int ObjectId { get; set; }
        public CurrencyAmountKey Key { get; set; }
        public double Amount { get; set; }
        public Currency Currency { get; set; }
    }
}
