using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Aggregations;
using StoreHouse360.Domain.Entities;
using StoreHouse360.Infrastructure.Persistence.Database;
using StoreHouse360.Infrastructure.Persistence.Database.Models;

namespace StoreHouse360.Infrastructure.Repositories
{
    public class ProductMovementRepository : RepositoryCrud<ProductMovement, ProductMovementDb>, IProductMovementRepository
    {
        public ProductMovementRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            
        }

        public IQueryable<AggregateProductQuantity> AggregateProductsQuantities(IList<int> productIds)
        {
            return dbSet
            .Include(movement => movement.Product)
            .Select(movement => new { movement.Id, movement.ProductId, movement.Product, movement.Quantity, movement.Type })
            .Where(movement => movement.ProductId != null && productIds.Contains((int)movement.ProductId!))
            .GroupBy(movement => movement.ProductId)
            .Select(movementsGrouping => new AggregateProductQuantity()
            {
                Product = mapper.Map<Product>(movementsGrouping.FirstOrDefault()!.Product),
                QuantitySum = movementsGrouping.Sum(movement => movement.Type == ProductMovementType.In ? movement.Quantity : -movement.Quantity)
            });
        }

        protected override IQueryable<ProductMovementDb> GetIncludedDatabaseSet()
        {
            return dbSet
                .Include(item => item.CurrencyAmounts!.Where(c => c.Key.Equals(CurrencyAmountKey.Movement)))
                .ThenInclude(currencyAmount => currencyAmount.Currency)
                .Include(item => item.Product)
                .Include(item => item.Currency);
        }
    }
}