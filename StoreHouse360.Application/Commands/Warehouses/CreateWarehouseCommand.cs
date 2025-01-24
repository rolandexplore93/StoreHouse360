using MediatR;
using StoreHouse360.Application.Commands.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Application.Repositories.UnitOfWork;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Warehouses
{
    public class CreateWarehouseCommand : ICreateEntityCommand<int>
    {
        public string Name { get; set; }
        public string Location { get; set; }
    }

    public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, int>
    {
        private readonly Lazy<IUnitOfWork> _unitOfWork;

        public CreateWarehouseCommandHandler(Lazy<IUnitOfWork> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            using var unitOfWork = _unitOfWork.Value;
                var cashDrawerAccount = new Account(
                id: default,
                code: "CD_" + request.Name[..2],
                name: "Cash Drawer " + request.Name,
                phone: "",
                city: request.Location
            );

            var accountSaveAction = await unitOfWork.AccountRepository.CreateAsync(cashDrawerAccount);
            cashDrawerAccount = await accountSaveAction();
            var warehouse = new Warehouse(
                id: default,
                name: request.Name,
                location: request.Location,
                cashDrawerAccountId: cashDrawerAccount.Id
            );
            
            var warehouseSaveAction = await unitOfWork.WarehouseRepository.CreateAsync(warehouse);
            warehouse = await warehouseSaveAction();
            await unitOfWork.CommitAsync();
            return warehouse.Id;
        }
    }
}
