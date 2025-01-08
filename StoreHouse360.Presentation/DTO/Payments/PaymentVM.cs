using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using StoreHouse360.DTO.Common;

namespace StoreHouse360.DTO.Payments
{
    public class PaymentVM : IViewModel, IMapFrom<Payment>
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string? Note { get; set; }
        public PaymentType PaymentType { get; set; }
        public PaymentIoType PaymentIoType { get; set; }
        public double Amount { get; set; }
        public int CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        public IEnumerable<CurrencyAmount> CurrencyAmounts { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
