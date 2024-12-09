using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Manufacturers
{
    public class GetAllManufacturersQuery : IRequest<IEnumerable<Manufacturer>>
    {
    }

    public class GetAllManufacturersQueryHandler : IRequestHandler<GetAllManufacturersQuery, IEnumerable<Manufacturer>>
    {
        private readonly IManufacturerRepository _manufacturerRepository;

        public GetAllManufacturersQueryHandler(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        public async Task<IEnumerable<Manufacturer>> Handle(GetAllManufacturersQuery request, CancellationToken cancellationToken)
        {
            var manufacturers = await _manufacturerRepository.GetAllAsync();
            return manufacturers;
        }
    }
}