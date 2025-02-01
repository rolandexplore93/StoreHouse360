using StoreHouse360.Application.Common.DTO;
using StoreHouse360.Domain.Aggregations;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Repositories
{
    public interface IProductMovementRepository : IRepositoryCrud<ProductMovement, int>
    {
        IQueryable<AggregateProductQuantity> AggregateProductsQuantities(ProductMovementFiltersDTO? filters = default);
        IQueryable<AggregateStoragePlaceQuantity> AggregateStoragePlacesQuantities(ProductMovementFiltersDTO? filters = default);
    }
}