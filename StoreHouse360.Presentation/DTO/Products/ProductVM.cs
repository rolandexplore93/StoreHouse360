using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using StoreHouse360.DTO.Common;

namespace StoreHouse360.DTO.Products
{
    public class ProductVM : IViewModel, IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }
        public int UnitId { get; set; }
        public string Barcode { get; set; }
        public double Price { get; set; }
        public int CurrencyId { get; set; }
        public int MinimumLevel { get; set; } = 0;
    }
}
