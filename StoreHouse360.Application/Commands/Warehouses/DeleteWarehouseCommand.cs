using StoreHouse360.Application.Commands.Common;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Warehouses
{
    [Authorize(Method = Method.Delete, Resource = Resource.Warehouses)]
    public class DeleteWarehouseCommand : DeleteEntityCommand<int>
    {

    }
    public class DeleteWarehouseCommandHandler : DeleteEntityCommandHandler<DeleteWarehouseCommand, Warehouse, int, IWarehouseRepository>
    {
        public DeleteWarehouseCommandHandler(IWarehouseRepository repository) : base(repository)
        {
        }
    }
}
