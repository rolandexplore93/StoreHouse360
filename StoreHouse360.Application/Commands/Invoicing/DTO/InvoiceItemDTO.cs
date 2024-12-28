using StoreHouse360.Application.Common.DTO;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.Application.Commands.Invoicing.DTO
{
    public class InvoiceItemDTO
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int CurrencyId { get; set; }
        [Required]
        public int PlaceId { get; set; }
        public string? Note { get; set; }
        public IEnumerable<CurrencyAmountDTO>? CurrencyAmounts { get; set; }
        public bool HasCurrencyAmount => CurrencyAmounts != null && CurrencyAmounts.Any();
    }
}
