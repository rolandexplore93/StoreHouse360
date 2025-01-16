using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Aggregations;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Products;
using StoreHouse360.DTO.StoragePlaces;

namespace StoreHouse360.DTO.StoragePlaceQuantity
{
    public class StoragePlaceQuantityVM : IViewModel, IMapFrom<AggregateStoragePlaceQuantity>
    {
        public ProductJoinedVM Product { get; set; }
        public StoragePlaceVM StoragePlace { get; set; }
        public int Quantity { get; set; }
    }
}
