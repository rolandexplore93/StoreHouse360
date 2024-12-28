using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StoreHouse360.Application.Exceptions;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;
using StoreHouse360.Infrastructure.Persistence.Database;
using StoreHouse360.Infrastructure.Persistence.Database.Models;
using StoreHouse360.Infrastructure.Persistence.Database.Models.Common;

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
        public async Task<SaveAction<Task<TEntity>>> CreateAsync(TEntity entity)
        {
            var model = MapEntityToModel(entity);
            var result = await dbSet.AddAsync(model);

            return async () =>
            {
                await SaveChanges();
                return MapModelToEntity(result.Entity);

            };
        }


        public async Task<SaveAction<Task<IEnumerable<TEntity>>>> CreateAllAsync(IEnumerable<TEntity> entities)
        {
            var models = entities.Select(MapEntityToModel);
            IList<EntityEntry<TModel>> results = new List<EntityEntry<TModel>>();

            foreach (var model in models)
            {
                results.Add(await dbSet.AddAsync(model));
            }

            return async () =>
            {
                await SaveChanges();
                return results.Select(result => MapModelToEntity(result.Entity));
            };
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(GetAllOptions<TEntity>? options = default)
        {
            IQueryable<TModel> databaseSet = options is { IncludeRelations: true } ? GetIncludedDatabaseSet() : dbSet;

            databaseSet = databaseSet.FilterSoftDeleteMethods();

            IQueryable<TEntity> entitiesSet = databaseSet.ProjectTo<TEntity>(mapper.ConfigurationProvider);

            if (options is { Filter : { } })
            {
                entitiesSet = entitiesSet.Where(options.Filter);
            }

            return entitiesSet;
        }

        public Task<TEntity> GetFirstAsync(Func<TEntity, bool> filter)
        {
            return dbSet.FirstAsync(model => filter(MapModelToEntity(model))).ContinueWith(task => MapModelToEntity(task.Result));
        }

        public async Task<TEntity> FindByIdAsync(TKey id, FindOptions? options = default)
        {
            try
            {
                var model = await GetModelById(id, options);
                return MapModelToEntity(model);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw new NotFoundException();
            }
        }

        private Task<TModel> GetModelById(TKey id, FindOptions? options = default)
        {
            IQueryable<TModel> databaseSet = options is { IncludeRelations: true } ? GetIncludedDatabaseSet() : dbSet;
            databaseSet = databaseSet.FilterSoftDeleteMethods();
            return databaseSet.FirstAsync(model => model.Id.Equals(id));
        }

        protected virtual IQueryable<TModel> GetIncludedDatabaseSet() => dbSet.AsQueryable();
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
