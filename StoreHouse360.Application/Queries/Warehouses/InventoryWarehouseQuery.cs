using MediatR;
using StoreHouse360.Application.Common.DTO;
using StoreHouse360.Application.Common.Models;
using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Aggregations;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Warehouses
{
    public class InventoryWarehouseQuery : GetPaginatedQuery<AggregateProductQuantity>
    {
        public ProductMovementFiltersDTO? Filters { get; set; }
    }
    public class InventoryWarehouseQueryHandler : IRequestHandler<InventoryWarehouseQuery, IPaginatedCollections<AggregateProductQuantity>>
    {
        private readonly IProductMovementRepository _productMovementRepository;
        private readonly IProductRepository _productRepository;

        public InventoryWarehouseQueryHandler(IProductMovementRepository productMovementRepository, IProductRepository productRepository)
        {
            _productMovementRepository = productMovementRepository;
            _productRepository = productRepository;
        }
        public async Task<IPaginatedCollections<AggregateProductQuantity>> Handle(InventoryWarehouseQuery request, CancellationToken cancellationToken)
        {
            var aggregates = _productMovementRepository.AggregateProductsQuantities(request.Filters).ToList();

            var productIds = aggregates.Select(aggregate => aggregate.Product!.Id);

            var products = await _productRepository.GetAllAsync(new GetAllOptions<Product> { IncludeRelations = true });

            var filteredProducts = products
                .Where(product => productIds.Any(id => product.Id == id))
                .ToList();

            var aggregatesWithFullProduct = aggregates
                .Zip(filteredProducts)
                .Select(entry => entry.First.AddProduct(entry.Second));

            var paginatedQuery = aggregatesWithFullProduct
                .AsQueryable()
                .AsPaginatedQuery(request.Page, request.PageSize);

            return paginatedQuery;
        }
    }
}
