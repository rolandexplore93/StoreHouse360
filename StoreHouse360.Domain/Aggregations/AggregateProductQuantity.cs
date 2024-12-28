using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Domain.Aggregations
{
    public class AggregateProductQuantity : AggregateRoot
    {
        public Product Product { get; set; }
        public int QuantitySum { get; set; }
        public bool ExceedsMinimumLevel(int requestedQuantity) => Product.HasMinimumLevel && (QuantitySum - requestedQuantity < Product.MinimumLevel);
    }
}
