using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using StoreHouse360.Application.Exceptions;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;
using StoreHouse360.Infrastructure.Persistence.Database;
using StoreHouse360.Infrastructure.Persistence.Database.Models;

namespace StoreHouse360.Infrastructure.Repositories
{
    public abstract class RepositoryCrud<TEntity, TModel> : RepositoryCrudBase<ApplicationDbContext, TEntity, int, TModel>
    where TEntity : BaseEntity<int>
    where TModel : class, IDatabaseModel
    {
        public RepositoryCrud(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }

    public abstract class RepositoryCrudBase<TContext, TEntity, TKey, TModel> : RepositoryBase<TContext>, IRepositoryCrud<TEntity, TKey>
    where TContext : DbContext
    where TEntity : BaseEntity<TKey>
    where TModel : class, IDatabaseModel
    where TKey : IEquatable<TKey>
    {
        protected readonly IMapper mapper;
        protected readonly DbSet<TModel> dbSet;
        public RepositoryCrudBase(TContext dbContext, IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
            dbSet = dbContext.Set<TModel>();
        }
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var model = MapEntityToModel(entity);
            var result = await dbSet.AddAsync(model);
            //await _dbContext.SaveChangesAsync();
            return MapModelToEntity(result.Entity);
            //return modelToEntity;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            //return await dbSet.AsQueryable().ProjectTo<TEntity>(mapper.ConfigurationProvider).ToListAsync();
            return await dbSet.ProjectTo<TEntity>(mapper.ConfigurationProvider).ToListAsync();
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

        public async Task<TEntity> FindByIdAsync(TKey id)
        {
            try
            {
                var model = await GetModelById(id);
                return MapModelToEntity(model);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw new NotFoundException();
            }
        }

        private Task<TModel> GetModelById(TKey id)
        {
            return dbSet.FirstAsync(model => model.Id.Equals(id));
        }
        protected TModel MapEntityToModel(TEntity entity)
        {
            var output = mapper.Map<TModel>(entity);
            return output;
        }
            
        protected TEntity MapModelToEntity(TModel model) => mapper.Map<TEntity>(model);

        public async Task DeleteAsync(TKey id)
        {
            try
            {
                var model = await GetModelById(id);
                dbSet.Remove(model);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw new NotFoundException();
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {
                var modelFromDatabase = await GetModelById(entity.Id);
                //return MapModelToEntity(modelFromDatabase);
                _dbContext.Entry(modelFromDatabase).CurrentValues.SetValues(entity);
                return entity;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw new NotFoundException();
            }
        }
    }
}
