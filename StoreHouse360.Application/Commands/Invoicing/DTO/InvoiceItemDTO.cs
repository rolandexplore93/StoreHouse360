using StoreHouse360.Application.Common.DTO;

namespace StoreHouse360.Application.Commands.Invoicing.DTO
{
    public class InvoiceItemDTO
    {
        public int ProductId { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int CurrencyId { get; set; }
        public int PlaceId { get; set; }
        public IEnumerable<CurrencyAmountDTO> CurrencyAmounts { get; set; }
    }
}
