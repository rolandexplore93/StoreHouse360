using MediatR;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Manufacturers
{
    [Authorize(Method = Method.Read, Resource = Resource.Manufacturers)]
    public class GetManufacturerQuery : IRequest<Manufacturer>
    {
        public int Id { get; set; }
    }

    public class GetManufacturerQueryHandler : IRequestHandler<GetManufacturerQuery, Manufacturer>
    {
        private readonly IManufacturerRepository _manufacturerRepository;

        public GetManufacturerQueryHandler(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        public async Task<Manufacturer> Handle(GetManufacturerQuery request, CancellationToken cancellationToken)
        {
            var manufacturer = await _manufacturerRepository.FindByIdAsync(request.Id);
            return manufacturer;
        }
    }
}
