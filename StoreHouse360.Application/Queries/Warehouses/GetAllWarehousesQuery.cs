using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Warehouses
{
    public class GetAllWarehousesQuery : IRequest<IEnumerable<Warehouse>>
    {

    }
    public class GetAllWarehousesQueryHandler : IRequestHandler<GetAllWarehousesQuery, IEnumerable<Warehouse>>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        public GetAllWarehousesQueryHandler(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }
        public async Task<IEnumerable<Warehouse>> Handle(GetAllWarehousesQuery request, CancellationToken cancellationToken)
        {
            return await _warehouseRepository.GetAllAsync();
        }
    }
}