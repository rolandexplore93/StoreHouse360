using StoreHouse360.Application.Commands.Invoicing.DTO;
using StoreHouse360.Application.Commands.Invoicing;
using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.DTO.Invoices
{
    public class CreateInvoiceRequestDTO : IMapFrom<CreateInvoiceCommand>
    {
        [Required]
        public int AccountId { get; set; }
        [Required]
        public int WarehouseId { get; set; }
        [Required]
        public int CurrencyId { get; set; }
        public string? Note { get; set; }
        [Required]
        public InvoiceType Type { get; set; }
        [Required]
        [MinLength(1)]
        public IEnumerable<InvoiceItemDTO> Items { get; set; }
    }
}
