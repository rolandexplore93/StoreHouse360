using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;
using StoreHouse360.Infrastructure.Persistence.Database;
using StoreHouse360.Infrastructure.Persistence.Database.Models;

namespace StoreHouse360.Infrastructure.Repositories
{
    public class StoragePlacesRepository : RepositoryCrud<StoragePlace, StoragePlaceDb>, IStoragePlaceRepository
    {
        public StoragePlacesRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            
        }

        protected override IQueryable<StoragePlaceDb> GetIncludedDatabaseSet()
        {
            return dbSet.Include(p => p.Warehouse).Include(p => p.Container);
        }
    }
}