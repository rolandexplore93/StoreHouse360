using MediatR;
using StoreHouse360.Application.Common.DTO;
using StoreHouse360.Application.Common.Models;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Aggregations;

namespace StoreHouse360.Application.Queries.StoragePlaces
{
    [Authorize(Method = Method.Read, Resource = Resource.Invoices)]
    public class InventoryStoragePlaceQuery : GetPaginatedQuery<AggregateStoragePlaceQuantity>
    {
        public ProductMovementFiltersDTO? Filters { get; set; }
        public InventoryStoragePlaceQuery(ProductMovementFiltersDTO filters)
        {
            Filters = filters;
        }
    }
    public class GetStoragePlaceInventoryQueryHandler : IRequestHandler<InventoryStoragePlaceQuery, IPaginatedCollections<AggregateStoragePlaceQuantity>>
    {
        private readonly IProductMovementRepository _productMovementRepository;
        public GetStoragePlaceInventoryQueryHandler(IProductMovementRepository productMovementRepository)
        {
            _productMovementRepository = productMovementRepository;
        }
        public async Task<IPaginatedCollections<AggregateStoragePlaceQuantity>> Handle(InventoryStoragePlaceQuery request, CancellationToken cancellationToken)
        {
            var aggregates = _productMovementRepository.AggregateStoragePlacesQuantities(request.Filters).AsPaginatedQuery(request.Page, request.PageSize);

            return aggregates;
        }
    }
}
