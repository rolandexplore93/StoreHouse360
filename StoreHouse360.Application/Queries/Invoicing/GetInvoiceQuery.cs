using MediatR;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Invoicing
{
    [Authorize(Method = Method.Read, Resource = Resource.Invoices)]
    public class GetInvoiceQuery : IRequest<Invoice>
    {
        public int Id { get; set; }
    }

    public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, Invoice>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public GetInvoiceQueryHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        public Task<Invoice> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
        {
            return _invoiceRepository.FindByIdAsync(request.Id, new FindOptions { IncludeRelations = true });
        }
    }
}