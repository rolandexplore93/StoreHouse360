using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;
using Unit = StoreHouse360.Domain.Entities.Unit;

namespace StoreHouse360.Application.Queries.Units
{
    public class GetAllUnitsQuery : IRequest<IEnumerable<Unit>>
    {
    }
    public class GetAllUnitsQueryHandler : IRequestHandler<GetAllUnitsQuery, IEnumerable<Unit>>
    {
        private readonly IUnitRepository _unitRepository;

        public GetAllUnitsQueryHandler(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }
        public async Task<IEnumerable<Unit>> Handle(GetAllUnitsQuery request, CancellationToken cancellationToken)
        {
            return await _unitRepository.GetAllAsync();
        }
    }
}
