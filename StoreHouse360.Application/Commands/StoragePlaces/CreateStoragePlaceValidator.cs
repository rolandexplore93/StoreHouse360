using FluentValidation;
using StoreHouse360.Application.Repositories;

namespace StoreHouse360.Application.Commands.StoragePlaces
{
    public class CreateStoragePlaceValidator : AbstractValidator<CreateStoragePlaceCommand>
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public CreateStoragePlaceValidator(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
            RuleFor(command => command.WarehouseId).MustAsync(_warehouseExists).WithMessage("Warehouse id is not found");
        }

        private Task<bool> _warehouseExists(int warehouseId, CancellationToken cancellationToken)
        {
            return _warehouseRepository.IsExistsById(warehouseId);
        }
    }
}
