using MediatR;
using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Manufacturers
{
    public class GetAllManufacturersQuery : GetPaginatedQuery<Manufacturer>
    {
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