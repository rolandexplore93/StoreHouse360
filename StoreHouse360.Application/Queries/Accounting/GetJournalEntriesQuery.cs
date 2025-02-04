using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Accounting
{
    public class GetJournalEntriesQuery : GetPaginatedQuery<Journal>
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
    }

    public class GetJournalEntriesHandler : PaginatedQueryHandler<GetJournalEntriesQuery, Journal>
    {
        private readonly IJournalRepository _journalRepository;
        public GetJournalEntriesHandler(IJournalRepository journalRepository)
        {
            _journalRepository = journalRepository;
        }
        protected override async Task<IQueryable<Journal>> GetQuery(GetJournalEntriesQuery request, CancellationToken cancellationToken)
        {
            var journals = await _journalRepository.GetAllAsync();
            var filteredJournals = journals.Where(journal => journal.SourceAccountId == request.FromAccountId || request.FromAccountId == default)
                .Where(journal => journal.AccountId == request.ToAccountId || request.ToAccountId == default);
            return filteredJournals;
        }
    }
}
