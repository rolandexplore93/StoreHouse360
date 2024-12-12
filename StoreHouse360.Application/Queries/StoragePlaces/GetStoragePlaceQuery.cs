﻿using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.StoragePlaces
{
    public class GetStoragePlaceQuery : IRequest<StoragePlace>
    {
        public int Id { get; set; }
    }
    public class GetStoragePlaceQueryHandler : IRequestHandler<GetStoragePlaceQuery, StoragePlace>
    {
        private readonly IStoragePlaceRepository _storagePlaceRepository;
        public GetStoragePlaceQueryHandler(IStoragePlaceRepository storagePlaceRepository)
        {
            _storagePlaceRepository = storagePlaceRepository;
        }
        public async Task<StoragePlace> Handle(GetStoragePlaceQuery request, CancellationToken cancellationToken)
        {
            return await _storagePlaceRepository.FindByIdAsync(request.Id, new FindOptions { IncludeRelations = true });
        }
    }
}