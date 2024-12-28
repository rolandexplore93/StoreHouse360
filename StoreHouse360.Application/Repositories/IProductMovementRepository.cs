using StoreHouse360.Domain.Aggregations;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Repositories
{
    public interface IProductMovementRepository : IRepositoryCrud<ProductMovement, int>
    {
        IQueryable<AggregateProductQuantity> AggregateProductsQuantities(IList<int> productIds);
    }
}