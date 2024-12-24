using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Infrastructure.Repositories
{
    public class ProductMovementRepository : IProductMovementRepository
    {
        public IQueryable<AggregatedProductQuantity> AggregatedProductQuantities(IList<int> productIds)
        {
            throw new NotImplementedException();
        }

        public Task<SaveAction<Task<ProductMovement>>> CreateAsync(ProductMovement entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductMovement> FindByIdAsync(int id, FindOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<ProductMovement>> GetAllAsync(GetAllOptions<ProductMovement>? options = null)
        {
            throw new NotImplementedException();
        }

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<ProductMovement> UpdateAsync(ProductMovement entity)
        {
            throw new NotImplementedException();
        }
    }
}
