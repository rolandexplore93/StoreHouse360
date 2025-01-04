using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Invoicing
{
    public class GetAllInvoicesQuery : GetPaginatedQuery<Invoice>
    {
        public int AccountId { get; set; } = default;
        public int WarehouseId { get; set; } = default;
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
            var filterInvoices = invoices
                                    .Where(invoice => invoice.WarehouseId == request.WarehouseId || request.WarehouseId == default)
                                    .Where(invoice => invoice.AccountId == request.AccountId || request.AccountId == default);
            return filterInvoices;
        }
    }
}
