using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Repositories
{
    public interface IJournalRepository : IRepositoryCrud<Journal, int>
    {
        public Task<SaveAction<Task<(Journal Debit, Journal Credit)>>> CreateJournals(Journal debit, Journal credit);
    }
}
