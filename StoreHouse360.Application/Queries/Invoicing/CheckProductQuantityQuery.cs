using MediatR;
using StoreHouse360.Application.Common.DTO;
using StoreHouse360.Application.Queries.Invoicing.DTO;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Aggregations;
using StoreHouse360.Domain.Exceptions;

namespace StoreHouse360.Application.Queries.Invoicing
{
    public class CheckProductQuantityQuery : IRequest<Unit>
    {
        public IEnumerable<CheckProductQuantityDTO> ProductQuantities;
    }

    /// <summary>
    /// <exception cref="ProductMinimumLevelExceededException"></exception>
    /// </summary>
    public class CheckProductQuantityQueryHandler : IRequestHandler<CheckProductQuantityQuery, Unit>
    {
        private readonly IProductMovementRepository _productMovementRepository;
        public CheckProductQuantityQueryHandler(IProductMovementRepository productMovementRepository)
        {
            _productMovementRepository = productMovementRepository;
        }

        public async Task<Unit> Handle(CheckProductQuantityQuery request, CancellationToken cancellationToken)
        {
            // Sort product quantities
            IList<CheckProductQuantityDTO> productQuantities = request.ProductQuantities
            .OrderBy(i => i.ProductId)
            .ToList();

            var productIds = productQuantities.Select(i => i.ProductId).ToList();

            // Check the products that exceed their minimum level
            IEnumerable<int> productIdsExceedsMinimumLevel = _productMovementRepository
                .AggregateProductsQuantities(new ProductMovementFiltersDTO { ProductIds = productIds })
                .ToList()
                .OrderBy(dto => dto.Product!.Id)
                .Zip(productQuantities)
                .Where(entry => _exceedsProductMinimumLevel(entry.First, entry.Second))
                .Select(entry => entry.First.Product!.Id)
                .ToList();

            // Throw an exception if there are products exceeding the minimum level
            if (productIdsExceedsMinimumLevel.Any())
            {
                throw new ProductMinimumLevelExceededException((IList<int>)productIdsExceedsMinimumLevel);
            }

            return Unit.Value;
        }
        private bool _exceedsProductMinimumLevel(AggregateProductQuantity aggregate, CheckProductQuantityDTO dto)
        {
            return aggregate.ExceedsMinimumLevel(dto.Quantity);
        }
    }

}
