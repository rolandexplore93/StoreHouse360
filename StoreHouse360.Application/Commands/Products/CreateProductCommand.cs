using StoreHouse360.Application.Commands.Common;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Products
{
    [Authorize(Method = Method.Write, Resource = Resource.Products)]
    public class CreateProductCommand : ICreateEntityCommand<int>
    {
        public string Name { get; init; }
        public int CategoryId { get; init; }
        public int ManufacturerId { get; init; }
        public int CountryOriginId { get; init; }
        public int UnitId { get; init; }
        public string Barcode { get; init; }
        public double Price { get; init; }
        public int CurrencyId { get; init; }
        public int? MinimumLevel { get; set; }
    }

    public class CreateProductCommandHandler : CreateEntityCommandHandler<CreateProductCommand, Product, int, IProductRepository>
    {
        public CreateProductCommandHandler(IProductRepository productRepository) : base(productRepository)
        {
        }

        protected override Product CreateEntity(CreateProductCommand request)
        {
            return new Product (
                id: default,
                name: request.Name,
                categoryId: request.CategoryId,
                manufacturerId: request.ManufacturerId,
                countryOriginId: request.CountryOriginId,
                unitId: request.UnitId,
                barcode: request.Barcode,
                price: request.Price,
                currencyId: request.CurrencyId,
                minimumLevel: request.MinimumLevel ?? 0
            
             );
        }
    }
}
