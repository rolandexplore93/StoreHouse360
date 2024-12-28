using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Manufacturers
{
    public class CreateManufacturerCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string? Code { get; set; }
    }

    public class CreateManufacturerCommandHandler : IRequestHandler<CreateManufacturerCommand, int>
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        public CreateManufacturerCommandHandler(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }
        public async Task<int> Handle(CreateManufacturerCommand request, CancellationToken cancellationToken)
        {
            var manufacturer = new Manufacturer
            {
                Name = request.Name,
                Code = request.Code
            };

            var saveAction = await _manufacturerRepository.CreateAsync(manufacturer);
            var createdManufacturer = await saveAction();
            return createdManufacturer.Id;
        }
    }
}
