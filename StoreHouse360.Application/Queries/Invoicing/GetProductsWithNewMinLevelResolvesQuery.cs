using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Invoicing
{
    public class GetProductsWithNewMinLevelResolvesQuery : IRequest<IEnumerable<Product>>
    {
        public int InvoiceId { get; }
        public GetProductsWithNewMinLevelResolvesQuery(int invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }
    public class GetProductsWithMinLevelResolvesQueryHandler : IRequestHandler<GetProductsWithNewMinLevelResolvesQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _productRepository;
        public GetProductsWithMinLevelResolvesQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<Product>> Handle(GetProductsWithNewMinLevelResolvesQuery request, CancellationToken cancellationToken)
        {
            return _productRepository.GetAllWithNewMinLevelResolved(request.InvoiceId).AsEnumerable();
        }
    }
}
