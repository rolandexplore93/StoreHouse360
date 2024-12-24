using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Infrastructure.Repositories
{
    public class CurrencyAmountRepository : ICurrencyAmountRepository
    {
        public Task<SaveAction<Task<CurrencyAmount>>> CreateAsync(CurrencyAmount entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CurrencyAmount> FindByIdAsync(int id, FindOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<CurrencyAmount>> GetAllAsync(GetAllOptions<CurrencyAmount>? options = null)
        {
            throw new NotImplementedException();
        }

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<CurrencyAmount> UpdateAsync(CurrencyAmount entity)
        {
            throw new NotImplementedException();
        }
    }
}
