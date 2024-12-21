using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.Application.Common.DTO
{
    public class CurrencyAmountDTO
    {
        [Required] public int CurrencyId { get; set; }
        [Required] public double Value { get; set; }
    }
}
