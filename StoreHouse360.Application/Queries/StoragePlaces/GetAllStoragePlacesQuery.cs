using MediatR;
using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.StoragePlaces
{
    public class GetAllStoragePlacesQuery : GetPaginatedQuery<StoragePlace>
    {
        public int? WarehouseId { get; set; }
        public int? ContainerId { get; set; }
        public bool? IsParent { get; set; }
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
            var query = await _storagePlaceRepository.GetAllAsync(new GetAllOptions<StoragePlace> { IncludeRelations = true });
            if (request.WarehouseId != null) query = query.Where(sp => sp.WarehouseId == request.WarehouseId);
            if (request.ContainerId != null) query = query.Where(sp => sp.ContainerId == request.ContainerId);
            if (request.IsParent != null) query = query.Where(sp => (bool)request.IsParent ? sp.ContainerId == null : sp.ContainerId != null);
            return query;
        }
    }
}