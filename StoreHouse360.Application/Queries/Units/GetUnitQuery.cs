using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;
using Unit = StoreHouse360.Domain.Entities.Unit;

namespace StoreHouse360.Application.Queries.Units
{
    public class GetUnitQuery : IRequest<Unit>
    {
        public int Id { get; init; }
    }
    public class GetUnitQueryHandler : IRequestHandler<GetUnitQuery, Unit>
    {
        private readonly IUnitRepository _unitRepository;
        public GetUnitQueryHandler(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }
        public async Task<Unit> Handle(GetUnitQuery request, CancellationToken cancellationToken)
        {
            return await _unitRepository.FindByIdAsync(request.Id);
        }
    }
}
