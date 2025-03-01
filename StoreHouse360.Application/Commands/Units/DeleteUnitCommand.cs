using StoreHouse360.Application.Commands.Common;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Units
{
    [Authorize(Method = Method.Delete, Resource = Resource.Units)]
    public class DeleteUnitCommand : DeleteEntityCommand<int>
    {

    }
    public class DeleteUnitCommandHandler : DeleteEntityCommandHandler<DeleteUnitCommand, Unit, int, IUnitRepository>
    {
        public DeleteUnitCommandHandler(IUnitRepository repository) : base(repository)
        {
        }
    }
}
