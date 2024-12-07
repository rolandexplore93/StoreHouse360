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

        /// <exception cref="NotFoundException"></exception>
        Task<TEntity> FindByIdAsync(TKey id);
        Task DeleteAsync(TKey id);
    }
}
