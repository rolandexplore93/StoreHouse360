using StoreHouse360.Application.Commands.Common;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.StoragePlaces
{
    [Authorize(Method = Method.Delete, Resource = Resource.Warehouses)]
    public class DeleteStoragePlaceCommand : DeleteEntityCommand<int>
    {

    }
    public class DeleteStoragePlaceCommandHandler : DeleteEntityCommandHandler<DeleteStoragePlaceCommand, StoragePlace, int, IStoragePlaceRepository>
    {
        public DeleteStoragePlaceCommandHandler(IStoragePlaceRepository repository) : base(repository)
        {
        }
    }
}
