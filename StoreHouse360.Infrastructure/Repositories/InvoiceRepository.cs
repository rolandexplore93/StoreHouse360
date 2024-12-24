using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Infrastructure.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public Task<SaveAction<Task<Invoice>>> CreateAsync(Invoice entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Invoice> FindByIdAsync(int id, FindOptions? options = null)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Invoice>> GetAllAsync(GetAllOptions<Invoice>? options = null)
        {
            throw new NotImplementedException();
        }

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<Invoice> UpdateAsync(Invoice entity)
        {
            throw new NotImplementedException();
        }
    }
}
