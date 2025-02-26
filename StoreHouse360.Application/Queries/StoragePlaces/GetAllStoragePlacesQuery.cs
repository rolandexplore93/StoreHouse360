using MediatR;
using StoreHouse360.Application.Common.QueryFilters;
using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.StoragePlaces
{
    public class GetAllStoragePlacesQuery : GetPaginatedQuery<StoragePlace>
    {
        [QueryFilter(QueryFilterCompareType.StringContains)]
        public string? Name { get; set; }

        [QueryFilter(QueryFilterCompareType.Equal)]
        public int? WarehouseId { get; set; }

        [QueryFilter(QueryFilterCompareType.Equal)]
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
            if (request.IsParent != null) query = query.Where(sp => (bool)request.IsParent ? sp.ContainerId == null : sp.ContainerId != null);
            return query;
        }

        protected override IQueryable<StoragePlace> ApplyFilters(IQueryable<StoragePlace> query, GetAllStoragePlacesQuery request)
        {
            var filterResult = base.ApplyFilters(query, request);
            if (request.IsParent != null) filterResult = filterResult.Where(sp => (bool)request.IsParent ? sp.ContainerId == null : sp.ContainerId != null);

            filterResult = filterResult.Where(storagePlace => storagePlace.WarehouseId == request.WarehouseId || request.WarehouseId == 0);

            return filterResult;
        }
    }
}