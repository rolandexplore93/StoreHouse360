using MediatR;
using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;
using Unit = StoreHouse360.Domain.Entities.Unit;

namespace StoreHouse360.Application.Queries.Units
{
    public class GetAllUnitsQuery : GetPaginatedQuery<Unit>
    {
    }
    public class GetAllUnitsQueryHandler : PaginatedQueryHandler<GetAllUnitsQuery, Unit>
    {
        private readonly IUnitRepository _unitRepository;

        public GetAllUnitsQueryHandler(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        protected override async Task<IQueryable<Unit>> GetQuery(GetAllUnitsQuery request, CancellationToken cancellationToken)
        {
            return await _unitRepository.GetAllAsync();
        }
    }
}
