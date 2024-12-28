using AutoMapper;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Infrastructure.Persistence.Database.Models;
using StoreHouse360.Infrastructure.Persistence.Database;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Infrastructure.Repositories
{
    public class UnitRepository : RepositoryCrud<Unit, UnitDb>, IUnitRepository
    {
        public UnitRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
