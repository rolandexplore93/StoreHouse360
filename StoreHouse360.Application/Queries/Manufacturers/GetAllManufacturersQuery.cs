using MediatR;
using StoreHouse360.Application.Common.QueryFilters;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Manufacturers
{
    [Authorize(Method = Method.Read, Resource = Resource.Manufacturers)]
    public class GetAllManufacturersQuery : GetPaginatedQuery<Manufacturer>
    {
        [QueryFilter(QueryFilterCompareType.StringContains)]
        public string? Name { get; set; }
    }

    public class GetAllManufacturersQueryHandler : PaginatedQueryHandler<GetAllManufacturersQuery, Manufacturer>
    {
        private readonly IManufacturerRepository _manufacturerRepository;

        public GetAllManufacturersQueryHandler(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        protected override async Task<IQueryable<Manufacturer>> GetQuery(GetAllManufacturersQuery request, CancellationToken cancellationToken)
        {
            var manufacturers = await _manufacturerRepository.GetAllAsync();
            return manufacturers;
        }
    }
}