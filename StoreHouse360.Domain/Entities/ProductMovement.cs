﻿namespace StoreHouse360.Domain.Entities
{
    public class ProductMovement : BaseEntity<int>
    {
        public int InvoiceId { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int PlaceId { get; set; }
        public StoragePlace Place { get; set; }

        public int Quantity { get; set; }

        public double UnitPrice { get; set; }

        public double TotalPrice => UnitPrice * Quantity;

        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public IEnumerable<CurrencyAmount>? CurrencyAmounts { get; set; }
        public ProductMovementType Type { get; set; }

        public string? Note { get; set; }

        public DateTime CreatedAt { get; set; }
        public void IncreaseQuantity(int quantity)
        {
            Quantity += quantity;
        }

        public static ProductMovementType TypeFromInvoice(InvoiceType invoiceType)
        {
            return invoiceType switch
            {
                InvoiceType.In => ProductMovementType.In,
                InvoiceType.Out => ProductMovementType.Out,
                _ => throw new NotImplementedException(),
            };
        }

    }
    public enum ProductMovementType
    {
        In, Out
    }
}
