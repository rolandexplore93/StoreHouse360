using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;
using StoreHouse360.Infrastructure.Persistence.Database;
using StoreHouse360.Infrastructure.Persistence.Database.Models;

namespace StoreHouse360.Infrastructure.Repositories
{
    public class JournalRepository : RepositoryCrud<Journal, JournalDb>, IJournalRepository
    {
        public JournalRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<SaveAction<Task<(Journal Debit, Journal Credit)>>> CreateJournals(Journal debit, Journal credit)
        {
            var creditModel = MapEntityToModel(credit);
            var debitModel = MapEntityToModel(debit);

            var resultCredit = await dbSet.AddAsync(creditModel);
            var resultDebit = await dbSet.AddAsync(debitModel);

            return async () =>
            {
                await SaveChanges();
                return (MapModelToEntity(resultCredit.Entity), MapModelToEntity(resultDebit.Entity));
            };
        }
    }
}
