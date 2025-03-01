using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.CurrencyAmounts;
using StoreHouse360.DTO.Products;
using StoreHouse360.DTO.StoragePlaces;

namespace StoreHouse360.DTO.Invoices
{
    public class ProductMovementVM : IViewModel, IMapFrom<ProductMovement>
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public ProductJoinedVM? Product { get; set; }
        public StoragePlaceVM Place { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public Currency Currency { get; set; }
        public IEnumerable<CurrencyAmountVM> CurrencyAmounts { get; set; }
        public string? Note { get; set; }
        public ProductMovementType Type { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
