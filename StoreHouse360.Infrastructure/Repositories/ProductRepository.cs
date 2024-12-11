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
    public class ProductRepository : RepositoryCrud<Product, ProductDb>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            
        }
        protected override IQueryable<ProductDb> GetIncludedDatabaseSet()
        {
            return dbSet
                .Include(p => p.Category)
                .Include(p => p.Currency)
                .Include(p => p.Manufacturer)
                .Include(p => p.Unit);
        }
    }
}