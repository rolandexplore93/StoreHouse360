using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Conversions
{
    public class GetAllConversionsQuery : GetPaginatedQuery<Conversion>
    {
    }

    public class GetAllConversionsQueryHandler : PaginatedQueryHandler<GetAllConversionsQuery, Conversion>
    {
        private readonly IConversionRepository _conversionRepository;

        public GetAllConversionsQueryHandler(IConversionRepository conversionRepository)
        {
            _conversionRepository = conversionRepository;
        }

        protected override async Task<IQueryable<Conversion>> GetQuery(GetAllConversionsQuery request, CancellationToken cancellationToken)
        {
            return await _conversionRepository.GetAllAsync();
        }
    }
}
