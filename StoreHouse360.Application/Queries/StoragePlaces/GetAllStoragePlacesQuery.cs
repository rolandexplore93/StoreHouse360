using MediatR;
using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.StoragePlaces
{
    public class GetAllStoragePlacesQuery : GetPaginatedQuery<StoragePlace>
    {
        public int? WarehouseId { get; set; }
    }
    public class GetAllStoragePlacesQueryHandler : PaginatedQueryHandler<GetAllStoragePlacesQuery, StoragePlace>
    {
        private readonly IStoragePlaceRepository _storagePlaceRepository;
        public GetAllStoragePlacesQueryHandler(IStoragePlaceRepository storagePlaceRepository)
        {
            _storagePlaceRepository = storagePlaceRepository;
        }

        protected override async Task<IQueryable<StoragePlace>> GetQuery(GetAllStoragePlacesQuery request, CancellationToken cancellationToken)
        {
            return await _storagePlaceRepository.GetAllAsync(new GetAllOptions<StoragePlace> { IncludeRelations = true, Filter = p => p.WarehouseId == request.WarehouseId });
        }
    }
}
