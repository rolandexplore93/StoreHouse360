using Microsoft.EntityFrameworkCore;
using StoreHouse360.Application.Repositories;

namespace StoreHouse360.Infrastructure.Repositories
{
    public abstract class RepositoryBase<TContext> : IRepositoryBase where TContext : DbContext
    {
        protected readonly TContext _dbContext;
        public RepositoryBase(TContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task SaveChanges()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
