using MediatR;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories;
using Unit = StoreHouse360.Domain.Entities.Unit;

namespace StoreHouse360.Application.Commands.Units
{
    [Authorize(Method = Method.Write, Resource = Resource.Units)]
    public class CreateUnitCommand : IRequest<int>
    {
        public string Name { get; init; }
        public int Value { get; set; }
    }
    public class CreateUnitCommandHandler : IRequestHandler<CreateUnitCommand, int>
    {
        private readonly IUnitRepository _repository;
        public CreateUnitCommandHandler(IUnitRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
        {
            var unit = new Unit
            {
                Name = request.Name,
                Value = request.Value
            };
            var saveAction = await _repository.CreateAsync(unit);
            var createdUnit = await saveAction();
            return createdUnit.Id;
        }
    }
}
