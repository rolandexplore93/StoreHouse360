using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using StoreHouse360.Application.Exceptions;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;
using StoreHouse360.Infrastructure.Persistence.Database;
using StoreHouse360.Infrastructure.Persistence.Database.Models;
using System;
using System.ComponentModel.DataAnnotations;

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
            await _dbContext.SaveChangesAsync();
            var modelToEntity = MapModelToEntity(result.Entity);
            return modelToEntity;
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

        public async Task<TEntity> FindByIdAsync(TKey id)
        {
            try
            {
                var model = await dbSet.FirstAsync(model => model.Id.Equals(id));
                return MapModelToEntity(model);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw new NotFoundException();
            }
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
                var model = await dbSet.FirstAsync(model => model.Equals(id));
                dbSet.Remove(model);
            }
            catch (InvalidOperationException ex)
            {
                throw new NotFoundException();
            }
        }
    }

}
