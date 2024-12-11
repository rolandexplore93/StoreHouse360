using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Repositories
{
    public interface IRepositoryBase
    {
        Task SaveChanges();
    }

    public interface IRepositoryCrud<TEntity, TKey> : IRepositoryBase where TEntity : BaseEntity<TKey>
    {
        Task<SaveAction<Task<TEntity>>> CreateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync(GetAllOptions? options = default);

        /// <exception cref="NotFoundException"></exception>
        Task<TEntity> FindByIdAsync(TKey id, FindOptions? options = default);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TKey id);
    }

    public delegate T SaveAction<T>();

    public class FindOptions
    {
        public bool IncludeRelations { get; set; } = false;
    }

    public class GetAllOptions
    {
        public bool IncludeRelations { get; set; } = false;
    }
}
