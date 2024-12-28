using StoreHouse360.Application.Commands.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Products
{
    public class UpdateProductCommand : IUpdateEntityCommand<int>
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public int CategoryId { get; init; }
        public int ManufacturerId { get; init; }
        public int UnitId { get; init; }
        public string Barcode { get; init; }
        public double Price { get; init; }
        public int CurrencyId { get; init; }
        public int? MinimumLevel { get; init; }
    }
    public class UpdateProductCommandHandler : UpdateEntityCommandHandler<UpdateProductCommand, Product, int, IProductRepository>
    {
        public UpdateProductCommandHandler(IProductRepository repository) : base(repository)
        {
        }
        protected override Product GetEntityToUpdate(UpdateProductCommand request)
        {
            return new Product
            {
                Id = request.Id,
                Name = request.Name,
                CategoryId = request.CategoryId,
                ManufacturerId = request.ManufacturerId,
                UnitId = request.UnitId,
                Barcode = request.Barcode,
                Price = request.Price,
                CurrencyId = request.CurrencyId,
                MinimumLevel = request.MinimumLevel ?? 0
            };
        }
    }
}
