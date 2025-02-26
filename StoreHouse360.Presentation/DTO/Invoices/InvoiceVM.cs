using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using StoreHouse360.DTO.Accounts;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Currencies;
using StoreHouse360.DTO.Warehouses;

namespace StoreHouse360.DTO.Invoices
{
    public class InvoiceVM : IViewModel, IMapFrom<Invoice>
    {
        public int Id { get; set; }
        public AccountVM? AccountId { get; set; } = default!;
        public WarehouseVM WarehouseId { get; set; } = default!;
        public CurrencyVM? CurrencyId { get; set; }
        public double TotalPrice { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public InvoiceStatus Status { get; set; }
        public InvoiceType Type { get; set; }
        public InvoiceAccountType AccountType { get; set; }
    }
}
