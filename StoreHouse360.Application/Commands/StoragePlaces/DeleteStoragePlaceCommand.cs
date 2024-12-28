using StoreHouse360.Application.Commands.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.StoragePlaces
{
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
