using AutoMapper;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;
using StoreHouse360.Infrastructure.Persistence.Database;
using StoreHouse360.Infrastructure.Persistence.Database.Models;

namespace StoreHouse360.Infrastructure.Repositories
{
    public class CategoryRepository : RepositoryCrud<Category, CategoryDb>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}