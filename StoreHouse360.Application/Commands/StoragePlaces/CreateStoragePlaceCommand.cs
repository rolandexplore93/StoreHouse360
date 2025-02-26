using StoreHouse360.Application.Commands.Common;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.StoragePlaces
{
    [Authorize(Method = Method.Write, Resource = Resource.Warehouses)]
    public class CreateStoragePlaceCommand : ICreateEntityCommand<int>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int WarehouseId { get; set; }
        public int? ContainerId { get; set; }
    }
    public class CreateStoragePlaceCommandHandler : CreateEntityCommandHandler<CreateStoragePlaceCommand, StoragePlace, int, IStoragePlaceRepository>
    {
        public CreateStoragePlaceCommandHandler(IStoragePlaceRepository repository) : base(repository)
        {
        }
        protected override StoragePlace CreateEntity(CreateStoragePlaceCommand request)
        {
            return new StoragePlace
            {
                WarehouseId = request.WarehouseId,
                ContainerId = request.ContainerId,
                Name = request.Name,
                Description = request.Description
            };
        }
    }
}