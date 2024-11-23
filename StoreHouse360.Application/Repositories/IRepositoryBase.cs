using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Repositories
{
    public interface IRepositoryBase
    {
        Task SaveChanges();
    }

    public interface IRepositoryCrud<TEntity, TKey> : IRepositoryBase where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> FindByIdAsync(TKey id);
    }
}
