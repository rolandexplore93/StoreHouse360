using StoreHouse360.Application.Commands.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Warehouses
{
    public class CreateWarehouseCommand : ICreateEntityCommand<int>
    {
        public string Name { get; set; }
        public string Location { get; set; }
    }
    public class CreateWarehouseCommandHandler : CreateEntityCommandHandler<CreateWarehouseCommand, Warehouse, int, IWarehouseRepository>
    {
        public CreateWarehouseCommandHandler(IWarehouseRepository repository) : base(repository)
        {
        }
        protected override Warehouse CreateEntity(CreateWarehouseCommand request)
        {
            return new Warehouse
            {
                Name = request.Name,
                Location = request.Location
            };
        }
    }
}
