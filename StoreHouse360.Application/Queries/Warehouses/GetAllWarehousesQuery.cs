using MediatR;
using StoreHouse360.Application.Common.QueryFilters;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Warehouses
{
    [Authorize(Method = Method.Read, Resource = Resource.Warehouses)]
    public class GetAllWarehousesQuery : GetPaginatedQuery<Warehouse>
    {
        [QueryFilter(QueryFilterCompareType.StringContains)]
        public string? Name { get; set; }
    }
    public class GetAllWarehousesQueryHandler : PaginatedQueryHandler<GetAllWarehousesQuery, Warehouse>
    {
        private readonly IWarehouseRepository _warehouseRepository;
        public GetAllWarehousesQueryHandler(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        protected override async Task<IQueryable<Warehouse>> GetQuery(GetAllWarehousesQuery request, CancellationToken cancellationToken)
        {
            return await _warehouseRepository.GetAllAsync();
        }
    }
}