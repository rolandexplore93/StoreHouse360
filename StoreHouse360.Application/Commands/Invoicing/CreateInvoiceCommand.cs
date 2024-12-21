using MediatR;
using StoreHouse360.Application.Commands.Invoicing.DTO;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Invoicing
{
    public class CreateInvoiceCommand : IRequest<int>
    {
        public int AccountId { get; set; }
        public int WarehouseId { get; set; }
        public string? Note { get; set; }
        public InvoiceType Type { get; set; }
        public IEnumerable<InvoiceItemDTO> Items { get; set; }
    }

}
