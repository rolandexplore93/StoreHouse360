using MediatR;
using StoreHouse360.Application.Common.Models;
using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Aggregations;

namespace StoreHouse360.Application.Queries.StoragePlaces
{
    public class InventoryStoragePlaceQuery : GetPaginatedQuery<AggregateStoragePlaceQuantity>
    {
        public int ProductId { get; }
        public int WarehouseId { get; }
        public int StoragePlaceId { get; }
        public InventoryStoragePlaceQuery(int productId, int warehouseId, int storagePlaceId)
        {
            ProductId = productId;
            WarehouseId = warehouseId;
            StoragePlaceId = storagePlaceId;
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
            var aggregates = _productMovementRepository.AggregateStoragePlacesQuantities(request.ProductId, request.WarehouseId, request.StoragePlaceId).AsPaginatedQuery(request.Page, request.PageSize);

            return aggregates;
        }
    }
}
