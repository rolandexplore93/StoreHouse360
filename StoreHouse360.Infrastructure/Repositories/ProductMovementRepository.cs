using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using StoreHouse360.Application.Common.DTO;
using StoreHouse360.Application.Common.QueryFilters;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Aggregations;
using StoreHouse360.Domain.Entities;
using StoreHouse360.Infrastructure.Persistence.Database;
using StoreHouse360.Infrastructure.Persistence.Database.Models;
using System.Collections.Generic;
using System.Reflection;

namespace StoreHouse360.Infrastructure.Repositories
{
    public class ProductMovementRepository : RepositoryCrud<ProductMovement, ProductMovementDb>, IProductMovementRepository
    {
        public ProductMovementRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            
        }

        public IQueryable<AggregateProductQuantity> AggregateProductsQuantities(ProductMovementFiltersDTO? filters = default)
        {
            var select = dbSet
                .Include(movement => movement.Invoice)
                .Select(movement => new AggregateProductMovement
                {
                    Id = movement.Id,
                    ProductId = movement.Product!.Id,
                    CategoryId = movement.Product.CategoryId,
                    ManufacturerId = movement.Product.ManufacturerId,
                    Quantity = movement.Quantity,
                    Type = movement.Type,
                    AccountId = movement.Invoice.AccountId,
                    WarehouseId = movement.Invoice.WarehouseId,
                    CreatedAt = movement.CreatedAt,
                    StoragePlaceId = movement.PlaceId,
                });

            if (filters != null)
            {
                select = select.WhereFilters(filters);
            }

            var aggregatesQuery = select
                .GroupBy(movement => movement.ProductId)
                .Select(movementsGrouping => new AggregateProductQuantity
                {
                    ProductId = movementsGrouping.Key,
                    QuantityInput = movementsGrouping.Where(movement => movement.Type == ProductMovementType.In).Sum(movement => movement.Quantity),
                    QuantityOutput = movementsGrouping.Where(movement => movement.Type == ProductMovementType.Out).Sum(movement => movement.Quantity),
                });

            var aggregates = aggregatesQuery.ToList();

            var productIds = aggregates.Select(aggregate => aggregate.ProductId);

            var products = _dbContext.Products.AsQueryable()
            .Include(p => p.Category)
            .Include(p => p.Currency)
            .Include(p => p.Manufacturer)
            .Include(p => p.Unit)
            .Where(product => productIds.Any(id => product.Id == id))
            .ProjectTo<Product>(mapper.ConfigurationProvider)
            .ToList();
            var aggregatesWithFullProduct = aggregates
                .Zip(products)
                .Select(entry => entry.First.AddProduct(entry.Second));
            return aggregatesWithFullProduct.AsQueryable();
        }

        public IQueryable<AggregateStoragePlaceQuantity> AggregateStoragePlacesQuantities(int productId, int warehouseId, int storagePlaceId)
        {
            var list = dbSet
                .Include(movement => movement.Product)
                    .ThenInclude(product => product!.Manufacturer)
                .Include(movement => movement.Product)
                    .ThenInclude(product => product!.Unit)
                .Include(movement => movement.Place)
                    .ThenInclude(storagePlace => storagePlace!.Warehouse)
                .Where(movement => movement.ProductId == productId || productId == default)
                .Where(movement => movement.Place!.Warehouse!.Id == warehouseId || warehouseId == default)
                .Where(movement => movement.Place!.Id == storagePlaceId || storagePlaceId == default)
                .ToList(); //TODO what's up with this? if you remove it, properties won't be included??
            return list
                .GroupBy(
                    movement => movement.PlaceId.GetValueOrDefault(),
                    movement => new
                    {
                        Product = movement.Product,
                        Quantity = movement.Type == ProductMovementType.In ? movement.Quantity : -movement.Quantity,
                        StoragePlace = movement.Place
                    },
                    (placeId, obj) => new
                    {
                        Product = obj.First().Product,
                        Quantity = obj.Sum(o => o.Quantity),
                        StoragePlace = obj.First().StoragePlace
                    }
                )
                .ToList()
                .Select(obj =>
                    new AggregateStoragePlaceQuantity(
                        mapper.Map<Product>(obj.Product),
                        obj.Quantity,
                        mapper.Map<StoragePlace>(obj.StoragePlace)
                    )
                )
                .AsQueryable();
        }

        protected override IQueryable<ProductMovementDb> GetIncludedDatabaseSet()
        {
            return dbSet
                .Include(item => item.CurrencyAmounts)!
                .ThenInclude(currencyAmount => currencyAmount.Currency)
                .Include(item => item.Product)
                .Include(item => item.Currency);
        }
    }

    public record AggregateProductMovement
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ProductDb? Product { get; set; }
        public int? CategoryId { get; set; }
        public int? ManufacturerId { get; set; }
        public int? AccountId { get; set; }
        public int? WarehouseId { get; set; }
        public int? StoragePlaceId { get; set; }
        public int Quantity { get; set; }
        public ProductMovementType Type { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}