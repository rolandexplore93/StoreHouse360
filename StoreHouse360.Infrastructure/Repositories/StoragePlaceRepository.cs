using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Infrastructure.Repositories
{
    public class StoragePlaceRepository : IStoragePlaceRepository
    {
        public Task<SaveAction<Task<StoragePlace>>> CreateAsync(StoragePlace entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<StoragePlace> FindByIdAsync(int id, FindOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<StoragePlace>> GetAllAsync(GetAllOptions<StoragePlace>? options = null)
        {
            throw new NotImplementedException();
        }

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<StoragePlace> UpdateAsync(StoragePlace entity)
        {
            throw new NotImplementedException();
        }
    }
}
