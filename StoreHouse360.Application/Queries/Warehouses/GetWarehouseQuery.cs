using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Warehouses
{
    public class GetWarehouseQuery : IRequest<Warehouse>
    {
        public int Id { get; set; }
    }
    public class GetWarehouseQueryHandler : IRequestHandler<GetWarehouseQuery, Warehouse>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        public GetWarehouseQueryHandler(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }
        public async Task<Warehouse> Handle(GetWarehouseQuery request, CancellationToken cancellationToken)
        {
            return await _warehouseRepository.FindByIdAsync(request.Id);
        }
    }
}
