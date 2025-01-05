using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Invoicing
{
    public class GetProductMovementsQuery : GetPaginatedQuery<ProductMovement>
    {
        public int? InvoiceId { get; set; }
    }
    public class GetProductMovementsQueryHandler : PaginatedQueryHandler<GetProductMovementsQuery, ProductMovement>
    {
        private readonly IProductMovementRepository _productMovementRepository;
        public GetProductMovementsQueryHandler(IProductMovementRepository productMovementRepository)
        {
            _productMovementRepository = productMovementRepository;
        }
        protected override async Task<IQueryable<ProductMovement>> GetQuery(GetProductMovementsQuery request, CancellationToken cancellationToken)
        {
            var productMovements = await _productMovementRepository.GetAllAsync(new GetAllOptions<ProductMovement> { IncludeRelations = true });
            return productMovements.Where(movement => request.InvoiceId == null || movement.InvoiceId.Equals(request.InvoiceId));
        }
    }
}