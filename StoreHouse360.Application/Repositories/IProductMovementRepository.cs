using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Repositories
{
    public interface IProductMovementRepository : IRepositoryCrud<ProductMovement, int>
    {
        IQueryable<AggregatedProductQuantity> AggregatedProductQuantities(IList<int> productIds);
    }

    public class AggregatedProductQuantity
    {
        public int ProductId { get; set; }
        public int QuantitySum { get; set; }
    }
}