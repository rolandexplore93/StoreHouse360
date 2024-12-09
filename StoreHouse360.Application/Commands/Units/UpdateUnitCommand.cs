using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;
using Unit = StoreHouse360.Domain.Entities.Unit;

namespace StoreHouse360.Application.Commands.Units
{
    public class UpdateUnitCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public int Value { get; set; }
    }
    public class UpdateUnitCommandHandler : IRequestHandler<UpdateUnitCommand, int>
    {
        private readonly IUnitRepository _unitRepository;
        public UpdateUnitCommandHandler(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }
        public async Task<int> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
        {
            var updatingEntity = new Unit { Id = request.Id, Name = request.Name, Value = request.Value };
            await _unitRepository.UpdateAsync(updatingEntity);
            await _unitRepository.SaveChanges();
            return updatingEntity.Id;
        }
    }
}
