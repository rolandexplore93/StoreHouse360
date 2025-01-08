using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Aggregations;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Products;

namespace StoreHouse360.DTO.ProductQuantity
{
    public class ProductQuantityVM : IViewModel, IMapFrom<AggregateProductQuantity>
    {
        public ProductJoinedVM? Product { get; set; }
        public int InputQuantities { get; set; }
        public int OutputQuantities { get; set; }
        public int QuantitySum { get; set; }
    }
}
