using AutoMapper;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;
using StoreHouse360.Infrastructure.Persistence.Database;
using StoreHouse360.Infrastructure.Persistence.Database.Models;

namespace StoreHouse360.Infrastructure.Repositories
{
    public class CurrencyAmountRepository : RepositoryCrud<CurrencyAmount, CurrencyAmountDb>, ICurrencyAmountRepository
    {
        public CurrencyAmountRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            
        }
    }
}
