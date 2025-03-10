﻿using MediatR;
using StoreHouse360.Application.Common.QueryFilters;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.CountryOrigins
{
    [Authorize(Method = Method.Read, Resource = Resource.Countries)]
    public class GetCountryOriginQuery : IRequest<CountryOrigin>
    {
        public int Id { get; init; }

        [QueryFilter(QueryFilterCompareType.StringContains)]
        public string? Name { get; set; }
    }
    public class GetCountryOriginQueryHandler : IRequestHandler<GetCountryOriginQuery, CountryOrigin>
    {
        private readonly ICountryOriginRepository _repository;
        public GetCountryOriginQueryHandler(ICountryOriginRepository repository)
        {
            _repository = repository;
        }
        public Task<CountryOrigin> Handle(GetCountryOriginQuery request, CancellationToken cancellationToken)
        {
            return _repository.FindByIdAsync(request.Id);
        }
    }
}
