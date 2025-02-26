using MediatR;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Products
{
    [Authorize(Method = Method.Read, Resource = Resource.Products)]
    public class GetProductQuery : IRequest<Product>
    {
        public int Id { get; set; }
    }
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product>
    {
        private readonly IProductRepository _productRepository;
        public GetProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            //var result = await _productRepository.FindIncludedByIdAsync(request.Id);
            var result = await _productRepository.FindByIdAsync(request.Id, options: new FindOptions { IncludeRelations = true });
            return result;
        }
    }
}