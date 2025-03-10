﻿using MediatR;
using StoreHouse360.Application.Common.QueryFilters;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;
using Unit = StoreHouse360.Domain.Entities.Unit;

namespace StoreHouse360.Application.Queries.Units
{
    [Authorize(Method = Method.Read, Resource = Resource.Units)]
    public class GetAllUnitsQuery : GetPaginatedQuery<Unit>
    {
        [QueryFilter(QueryFilterCompareType.StringContains)]
        public string? Name { get; set; }
    }
    public class GetAllUnitsQueryHandler : PaginatedQueryHandler<GetAllUnitsQuery, Unit>
    {
        private readonly IUnitRepository _unitRepository;

        public GetAllUnitsQueryHandler(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        protected override async Task<IQueryable<Unit>> GetQuery(GetAllUnitsQuery request, CancellationToken cancellationToken)
        {
            return await _unitRepository.GetAllAsync();
        }
    }
}
