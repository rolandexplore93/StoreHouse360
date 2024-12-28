using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Invoicing
{
    public class GetAllInvoicesQuery : GetPaginatedQuery<Invoice>
    {
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
            return await _invoiceRepository.GetAllAsync(new GetAllOptions<Invoice> { IncludeRelations = true });
        }
    }
}
