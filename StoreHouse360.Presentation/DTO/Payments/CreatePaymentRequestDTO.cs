using StoreHouse360.Application.Commands.Payments;
using StoreHouse360.Application.Common.DTO;
using StoreHouse360.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.DTO.Payments
{
    public class CreatePaymentRequestDTO : IMapFrom<CreatePaymentCommand>
    {
        [Required]
        public int InvoiceId { get; set; }
        public string? Note { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public int CurrencyId { get; set; }
        public IEnumerable<CurrencyAmountDTO>? CurrencyAmounts { get; set; }
    }
}
