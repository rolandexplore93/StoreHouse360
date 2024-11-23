using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.Infrastructure.Repositories
{
    public abstract class RepositoryCrudBase<TContext, TEntity, TKey, TModel> : RepositoryBase<TContext>,
    IRepositoryCrud<TEntity, TKey>
    where TContext : DbContext
    where TEntity : BaseEntity<TKey>
    where TModel : class
    where TKey : IEquatable<TKey>
    {
        protected readonly IMapper mapper;
        protected readonly DbSet<TModel> dbSet;
        public RepositoryCrudBase(TContext dbContext, IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
            dbSet = dbContext.Set<TModel>();
        }
        public Task<TEntity> CreateAsync(TEntity entity)
        {
            var model = MapEntityToModel(entity);
            var result = dbSet.AddAsync(model);
            return result.AsTask()
                .ContinueWith(task => MapModelToEntity(task.Result.Entity));
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.AsQueryable().ProjectTo<TEntity>(mapper.ConfigurationProvider).ToListAsync();
        }

        protected IEnumerable<TEntity> GetAllFiltered(Func<TModel, bool> filter)
        {
            return dbSet.Where(model => filter(model)).ProjectTo<TEntity>(mapper.ConfigurationProvider);
        }
        public Task<TEntity> GetFirstAsync(Func<TEntity, bool> filter)
        {
            return dbSet.FirstAsync(model => filter(MapModelToEntity(model)))
                .ContinueWith(task => MapModelToEntity(task.Result));
        }


        public Task<TEntity> FindByIdAsync(TKey id)
        {
            var field = typeof(TEntity).GetField("Id");

            if (field == null)
            {
                throw new ValidationException(typeof(TModel) + " has no Id field");
            }

            return dbSet.FirstAsync(model => field.GetValue(model)!.Equals(id)).ContinueWith(task => MapModelToEntity(task.Result));
        }
        protected TModel MapEntityToModel(TEntity entity) =>
            mapper.Map<TModel>(entity);
        protected TEntity MapModelToEntity(TModel model) =>
            mapper.Map<TEntity>(model);
    }

}
