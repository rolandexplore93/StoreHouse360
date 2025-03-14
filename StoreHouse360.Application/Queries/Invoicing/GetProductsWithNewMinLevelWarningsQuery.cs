﻿using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Invoicing
{
    public class GetProductsWithNewMinLevelWarningsQuery : IRequest<IEnumerable<Product>>
    {
        public int InvoiceId { get; }
        public GetProductsWithNewMinLevelWarningsQuery(int invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }
    public class getProductsWithMinLevelWarningsQueryHandler : IRequestHandler<GetProductsWithNewMinLevelWarningsQuery, IEnumerable<Product>>
    {
        private readonly IProductMovementRepository _movementRepository;
        private readonly IProductRepository _productRepository;
        public getProductsWithMinLevelWarningsQueryHandler(IProductMovementRepository movementRepository, IProductRepository productRepository)
        {
            _movementRepository = movementRepository;
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<Product>> Handle(GetProductsWithNewMinLevelWarningsQuery request, CancellationToken cancellationToken)
        {
            return _productRepository.GetAllWithNewMinLevelWarnings(request.InvoiceId).AsEnumerable();
        }
    }
}
