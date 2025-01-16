using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Domain.Aggregations
{
    public class AggregateStoragePlaceQuantity : AggregateRoot
    {
        public Product Product { get; }
        public int Quantity { get; }
        public StoragePlace StoragePlace { get; }
        public AggregateStoragePlaceQuantity(Product product, int quantity, StoragePlace storagePlace)
        {
            Product = product;
            Quantity = quantity;
            StoragePlace = storagePlace;
        }
    }
}
