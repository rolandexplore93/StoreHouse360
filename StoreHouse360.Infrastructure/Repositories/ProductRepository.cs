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
        public async Task<Product> FindIncludedByIdAsync(int id)
        {
            try
            {
                var productDb = await GetIncludedQuery().FirstAsync();
                return MapModelToEntity(productDb);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw new NotFoundException();
            }
        }

        public IEnumerable<Product> GetAllIncluded()
        {
            // Map Queryable Product model  to Entity using ProjectTo QueryableExtensions
            return GetIncludedQuery().ProjectTo<Product>(mapper.ConfigurationProvider);
        }

        private IQueryable<ProductDb> GetIncludedQuery()
        {
            return dbSet
                .Include(p => p.Category)
                .Include(p => p.Currency)
                .Include(p => p.Manufacturer)
                .Include(p => p.Unit)
                .AsQueryable();
        }
    }
}
