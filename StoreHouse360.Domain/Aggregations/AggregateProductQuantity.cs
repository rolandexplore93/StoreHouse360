﻿using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Domain.Aggregations
{
    public class AggregateProductQuantity : AggregateRoot
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int QuantityInput { get; set; }
        public int QuantityOutput { get; set; }
        public int QuantitySum => QuantityInput - QuantityOutput;
        public bool ExceedsMinimumLevel(int requestedQuantity) => Product!.HasMinimumLevel && (QuantitySum - requestedQuantity < Product.MinimumLevel);
        public bool ExceedsZeroLevel(int requestedQuantity) => QuantitySum - requestedQuantity < 0;
        public AggregateProductQuantity AddProduct(Product product)
        {
            Product = product;
            return this;
        }
    }
}
