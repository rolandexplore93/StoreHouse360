using AutoMapper;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;
using StoreHouse360.Infrastructure.Persistence.Database.Models;
using StoreHouse360.Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace StoreHouse360.Infrastructure.Repositories
{
    public class PaymentRepository : RepositoryCrud<Payment, PaymentDb>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        protected override IQueryable<PaymentDb> GetIncludedDatabaseSet()
        {
            return dbSet
                .Include(payment => payment.Currency)
                .IncludeFilter(payment => payment.CurrencyAmounts.Where(p => p.Key == CurrencyAmountKey.Payment));
        }
    }
}
