using MediatR;
using StoreHouse360.Application.Common.QueryFilters;
using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Products
{
    public class GetAllProductsQuery : GetPaginatedQuery<Product>
    {
        [QueryFilter(QueryFilterCompareType.StringContains)]
        public string? Name { get; set; }
    }
    public class GetAllProductsQueryHandler : PaginatedQueryHandler<GetAllProductsQuery, Product>
    {
        private readonly IProductRepository _productRepository;
        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        protected override async Task<IQueryable<Product>> GetQuery(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetAllAsync(new GetAllOptions<Product> { IncludeRelations = true });
        }
    }
}