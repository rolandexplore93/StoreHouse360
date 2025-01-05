using MediatR;
using StoreHouse360.Application.Common.DTO;
using StoreHouse360.Application.Common.Models;
using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Aggregations;

namespace StoreHouse360.Application.Queries.Warehouses
{
    public class InventoryWarehouseQuery : GetPaginatedQuery<AggregateProductQuantity>
    {
        public ProductMovementFiltersDTO? Filters { get; set; }
    }
    public class InventoryWarehouseQueryHandler : IRequestHandler<InventoryWarehouseQuery, IPaginatedCollections<AggregateProductQuantity>>
    {
        private readonly IProductMovementRepository _productMovementRepository;
        public InventoryWarehouseQueryHandler(IProductMovementRepository productMovementRepository)
        {
            _productMovementRepository = productMovementRepository;
        }
        public async Task<IPaginatedCollections<AggregateProductQuantity>> Handle(InventoryWarehouseQuery request, CancellationToken cancellationToken)
        {
            return  _productMovementRepository.AggregateProductsQuantities(request.Filters).AsPaginatedQuery(request.Page, request.PageSize);
        }
    }
}
