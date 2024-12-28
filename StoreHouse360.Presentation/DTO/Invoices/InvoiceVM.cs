using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using StoreHouse360.DTO.Common;

namespace StoreHouse360.DTO.Invoices
{
    public class InvoiceVM : IViewModel, IMapFrom<Invoice>
    {
        public int AccountId { get; set; }
        public int WarehouseId { get; set; }
        public int CurrencyId { get; set; }
        public double TotalPrice { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public InvoiceStatus Status { get; set; }
        public InvoiceType Type { get; set; }
    }
}
