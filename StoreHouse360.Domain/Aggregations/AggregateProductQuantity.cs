using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Domain.Aggregations
{
    public class AggregateProductQuantity : AggregateRoot
    {
        public Product? Product { get; set; }
        public int QuantityInput { get; set; }
        public int QuantityOutput { get; set; }
        public int QuantitySum => QuantityInput - QuantityOutput;
        public bool ExceedsMinimumLevel(int requestedQuantity) => Product!.HasMinimumLevel && (QuantitySum - requestedQuantity < Product.MinimumLevel);
        public AggregateProductQuantity AddProduct(Product product)
        {
            Product = product;
            return this;
        }
    }
}
