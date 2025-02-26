using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Invoicing
{
    [Authorize(Method = Method.Read, Resource = Resource.Invoices)]
    public class GetAllInvoicesQuery : GetPaginatedQuery<Invoice>
    {
        public int? AccountId { get; set; } = default;
        public int? WarehouseId { get; set; } = default;
        public InvoiceType? Type { get; set; }
        public InvoiceAccountType? AccountType { get; set; }
    }
    public class GetAllInvoicesQueryHandler : PaginatedQueryHandler<GetAllInvoicesQuery, Invoice>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public GetAllInvoicesQueryHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        protected override async Task<IQueryable<Invoice>> GetQuery(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = await _invoiceRepository.GetAllAsync(new GetAllOptions<Invoice> { IncludeRelations = true });
            var sortInvoice = invoices.OrderByDescending(invoice  => invoice.CreatedAt);
            return _applyFilters(sortInvoice, request);
        }

        private IQueryable<Invoice> _applyFilters(IQueryable<Invoice> query, GetAllInvoicesQuery request)
        {
            if (request.Type is not null)
                query = query.Where(invoice => invoice.Type == request.Type);
            if (request.AccountType is not null)
                query = query.Where(invoice => invoice.AccountType == request.AccountType);
            if (request.WarehouseId is not null)
                query = query.Where(invoice => invoice.WarehouseId == request.WarehouseId);
            if (request.AccountId is not null)
                query = query.Where(invoice => invoice.AccountId == request.AccountId);
            return query;
        }
    }
}
