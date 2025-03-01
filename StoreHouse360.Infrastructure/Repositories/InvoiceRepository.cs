using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;
using StoreHouse360.Infrastructure.Persistence.Database;
using StoreHouse360.Infrastructure.Persistence.Database.Models;

namespace StoreHouse360.Infrastructure.Repositories
{
    public class InvoiceRepository : RepositoryCrud<Invoice, InvoiceDb>, IInvoiceRepository
    {
        public InvoiceRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            
        }

        protected override IQueryable<InvoiceDb> GetIncludedDatabaseSet()
        {
            return base.GetIncludedDatabaseSet()
                .Include(invoice => invoice.Account)
                .Include(invoice => invoice.Currency)
                .Include(invoice => invoice.Warehouse);
        }
    }
}
