using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Application.Queries.Accounting;
using StoreHouse360.DTO.Pagination;

namespace StoreHouse360.DTO.Journals
{
    public class GetAllJournalsQueryParams : PaginationRequestParams, IMapFrom<GetJournalEntriesQuery>
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
    }
    public class GetAllCashDrawerJournalsQueryParams : PaginationRequestParams, IMapFrom<GetJournalEntriesQuery>
    {
        public int ToAccountId { get; set; }
    }
}
