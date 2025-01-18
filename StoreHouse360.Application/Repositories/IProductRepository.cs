using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Repositories
{
    public interface IProductRepository : IRepositoryCrud<Product, int>
    {
        IQueryable<Product> GetAllWithNewMinLevelWarnings(int invoiceId);
        IQueryable<Product> GetAllWithNewMinLevelResolved(int invoiceId);
        IQueryable<Product> GetAllInStoragePlace(int storagePlaceId, bool includeStoragePlaceChildren = false);
    }
}
