﻿using StoreHouse360.Domain.Exceptions;

namespace StoreHouse360.Domain.Entities
{
    public class Invoice : BaseEntity<int>
    {
        public int? AccountId { get; set; }
        public Account? Account { get; set; }

        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

        public int? CurrencyId { get; set; }
        public Currency? Currency { get; set; }

        public double TotalPrice { get; set; }
        public string? Note { get; set; }

        public DateTime CreatedAt { get; set; }
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Closed; // Invoice remains closed until a product is added
        public InvoiceType Type { get; set; }

        public IList<ProductMovement> Items { get; set; } = new List<ProductMovement>();

        public void AddItem(ProductMovement item)
        {
            TotalPrice += item.TotalPrice;

            if (AddedProductOpensInvoice())
            {
                Open();
            }

            Items.Add(item);
        }
        private bool AddedProductOpensInvoice() => TotalPrice != 0 && Status == InvoiceStatus.Closed;
        public bool ProductExists(int productId) => Items.Any(i => i.ProductId == productId);
        public bool IsClosed() => Status == InvoiceStatus.Closed;
        public void Close()
        {
            if (Status == InvoiceStatus.Closed)
            {
                throw new InvoiceClosedException();
            }
            Status = InvoiceStatus.Closed;
        }

        public void Open()
        {
            if (Status == InvoiceStatus.Opened)
            {
                throw new InvoiceOpenedException();
            }
            Status = InvoiceStatus.Opened;
        }

    }

    public enum InvoiceStatus
    {
        Opened, Closed
    }

    public enum InvoiceType
    {
        In, Out
    }
}
