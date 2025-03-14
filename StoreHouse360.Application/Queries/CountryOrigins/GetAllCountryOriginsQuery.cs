﻿using StoreHouse360.Application.Common.QueryFilters;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.CountryOrigins
{
    [Authorize(Method = Method.Read, Resource = Resource.Countries)]
    public class GetAllCountryOriginsQuery : GetPaginatedQuery<CountryOrigin>
    {
        [QueryFilter(QueryFilterCompareType.StringContains)]
        public string? Name { get; set; }
    }
    public class GetAllCountryOriginsQueryHandler : PaginatedQueryHandler<GetAllCountryOriginsQuery, CountryOrigin>
    {
        private readonly ICountryOriginRepository _repository;

        public GetAllCountryOriginsQueryHandler(ICountryOriginRepository repository)
        {
            _repository = repository;
        }
        protected override async Task<IQueryable<CountryOrigin>> GetQuery(GetAllCountryOriginsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
