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

        public IQueryable<Product> GetAllWithNewMinLevelWarnings(int invoiceId)
        {
            var invoiceMovements = _dbContext.ProductMovements
                .Where(movement => movement.InvoiceId == invoiceId)
                .ToList();

            var invoiceProductIds = invoiceMovements
                .Select(movement => movement.ProductId.GetValueOrDefault())
                .ToList();

            var invoiceProductIdsAndQuantities = _dbContext.ProductMovements
                .Where(movement => invoiceProductIds.Any(productId => productId == movement.ProductId.GetValueOrDefault()))
                .GroupBy(
                    movement => movement.ProductId.GetValueOrDefault(),
                    m => m.Type == ProductMovementType.In ? m.Quantity : -m.Quantity,
                    (productId, quantities) => new { ProductId = productId, quantity = quantities.Sum() }
                )
                .ToList();

            var invoiceProductIdsAndQuantitiesBeforeInvoice = invoiceProductIdsAndQuantities
                .Zip(invoiceMovements)
                .Select(entry => new
                {
                    ProductId = entry.First.ProductId,
                    quantityBeforeInvoice = entry.First.quantity + entry.Second.Quantity
                });

            var products = _dbContext.Products
                .Where(product => invoiceProductIds.Any(productId => productId == product.Id))
                .ToList();

            var invoiceProductIdsWithMinLevelNotExceededBeforeInvoice = invoiceProductIdsAndQuantitiesBeforeInvoice
                .Zip(products)
                .Where(entry => entry.First.quantityBeforeInvoice >= entry.Second.MinimumLevel)
                .Select(entry => entry.First.ProductId);

            var invoiceProductsWithNewMinLevelWarnings = invoiceProductIdsAndQuantities
                .Where(entry => invoiceProductIdsWithMinLevelNotExceededBeforeInvoice.Contains(entry.ProductId))
                .Zip(products)
                .Where(entry => entry.Second.MinimumLevel > entry.First.quantity)
                .Select(entry => entry.Second);

            return invoiceProductsWithNewMinLevelWarnings
                .AsQueryable()
                .ProjectTo<Product>(mapper.ConfigurationProvider);
        }
        public IQueryable<Product> GetAllWithNewMinLevelResolved(int invoiceId)
        {
            var invoiceMovements = _dbContext.ProductMovements
                .Where(movement => movement.InvoiceId == invoiceId)
                .ToList();

            var invoiceProductIds = invoiceMovements
                .Select(movement => movement.ProductId.GetValueOrDefault())
                .ToList();

            var invoiceProductIdsAndQuantities = _dbContext.ProductMovements
                .Where(movement => invoiceProductIds.Any(productId => productId == movement.ProductId.GetValueOrDefault()))
                .GroupBy(
                    movement => movement.ProductId.GetValueOrDefault(),
                    m => m.Type == ProductMovementType.In ? m.Quantity : -m.Quantity,
                    (productId, quantities) => new { ProductId = productId, quantity = quantities.Sum() }
                )
                .ToList();

            var invoiceProductIdsAndQuantitiesBeforeInvoice = invoiceProductIdsAndQuantities
                .Zip(invoiceMovements)
                .Select(entry => new
                {
                    ProductId = entry.First.ProductId,
                    quantityBeforeInvoice = entry.First.quantity - entry.Second.Quantity
                });

            var products = _dbContext.Products
                .Where(product => invoiceProductIds.Any(productId => productId == product.Id))
                .ToList();

            var invoiceProductIdsWithMinLevelExceededBeforeInvoice = invoiceProductIdsAndQuantitiesBeforeInvoice
                .Zip(products)
                .Where(entry => entry.First.quantityBeforeInvoice < entry.Second.MinimumLevel)
                .Select(entry => entry.First.ProductId);

            var invoiceProductsWithNewMinLevelResolves = invoiceProductIdsAndQuantities
                .Where(entry => invoiceProductIdsWithMinLevelExceededBeforeInvoice.Contains(entry.ProductId))
                .Zip(products)
                .Where(entry => entry.Second.MinimumLevel <= entry.First.quantity)
                .Select(entry => entry.Second);

            return invoiceProductsWithNewMinLevelResolves
                .AsQueryable()
                .ProjectTo<Product>(mapper.ConfigurationProvider);
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