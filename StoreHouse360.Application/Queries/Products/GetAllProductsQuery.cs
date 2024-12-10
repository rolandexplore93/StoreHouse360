using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Products
{
    public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _productRepository;
        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_productRepository.GetAllIncluded());
        }
    }
}