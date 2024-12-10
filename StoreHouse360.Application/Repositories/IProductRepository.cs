using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Repositories
{
    public interface IProductRepository : IRepositoryCrud<Product, int>
    {
        public Task<Product> FindIncludedByIdAsync(int id);
        public IEnumerable<Product> GetAllIncluded();
    }
}
