using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using StoreHouse360.DTO.Common;

namespace StoreHouse360.DTO.Products
{
    public class ProductJoinedVM : IViewModel, IMapFrom<Product>
    {
        public string Name { get; set; }
        public Category Category { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public Unit Unit { get; set; }
        public string Barcode { get; set; }
        public double Price { get; set; }
        public Currency Currency { get; set; }
    }
}
