using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.StoragePlaces
{
    public class GetAllStoragePlacesQuery : IRequest<IEnumerable<StoragePlace>>
    {
        public int? WarehouseId { get; set; }
    }
    public class GetAllStoragePlacesQueryHandler : IRequestHandler<GetAllStoragePlacesQuery, IEnumerable<StoragePlace>>
    {
        private readonly IStoragePlaceRepository _storagePlaceRepository;
        public GetAllStoragePlacesQueryHandler(IStoragePlaceRepository storagePlaceRepository)
        {
            _storagePlaceRepository = storagePlaceRepository;
        }
        public async Task<IEnumerable<StoragePlace>> Handle(GetAllStoragePlacesQuery request, CancellationToken cancellationToken)
        {
            return await _storagePlaceRepository.GetAllAsync(new GetAllOptions<StoragePlace> { IncludeRelations = true, Filter = p => p.WarehouseId == request.WarehouseId });
        }
    }
}
