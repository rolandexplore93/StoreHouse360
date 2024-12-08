using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Manufacturers
{
    public class CreateManufacturerCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string? Code { get; set; }
    }

    public class CreateManufacturerCommandHandler : IRequestHandler<CreateManufacturerCommand, string>
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        public CreateManufacturerCommandHandler(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }
        public async Task<string> Handle(CreateManufacturerCommand request, CancellationToken cancellationToken)
        {
            var manufacturer = new Manufacturer
            {
                Name = request.Name,
                Code = request.Code
            };
            var createdManufacturer = await _manufacturerRepository.CreateAsync(manufacturer);
            await _manufacturerRepository.SaveChanges();
            return $"Manufacturer Account ({createdManufacturer.Name}) created sucessfully.";
        }
    }
}
