using StoreHouse360.Application.Repositories;
using StoreHouse360.Application.Repositories.Aggregates;
using StoreHouse360.Domain.Aggregations;
using StoreHouse360.Domain.Entities;
using StoreHouse360.Infrastructure.Persistence.Database;

namespace StoreHouse360.Infrastructure.Repositories.Aggregates
{
    public class InvoicePaymentsRepository : RepositoryBase<ApplicationDbContext>, IInvoicePaymentsRepository
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IPaymentRepository _paymentRepository;
        public InvoicePaymentsRepository(ApplicationDbContext dbContext, IInvoiceRepository invoiceRepository, IPaymentRepository paymentRepository) : base(dbContext)
        {
            _invoiceRepository = invoiceRepository;
            _paymentRepository = paymentRepository;
        }

        
        public async Task<InvoicePayments> FindByInvoiceId(int invoiceId)
        {
            var invoice = await _invoiceRepository.FindByIdAsync(invoiceId);
            var payments = await _paymentRepository.GetAllAsync();
            var filteredPayments = payments.Where(p => p.InvoiceId == invoiceId).ToList();
            return new InvoicePayments { Invoice = invoice, Payments = payments };
        }

        public async Task<SaveAction<Task<InvoicePayments>>> Save(InvoicePayments invoicePayments)
        {
            var saveAction = await _paymentRepository.CreateAllAsync(invoicePayments.PendingPayments);
            return async () =>
            {
                var savedPayments = await saveAction();

                Invoice updatedInvoice = await _invoiceRepository.UpdateAsync(invoicePayments.Invoice);

                await _invoiceRepository.SaveChanges();

                return new InvoicePayments { Invoice = updatedInvoice, Payments = invoicePayments.Payments.Concat(savedPayments) };
            };
        }

    }
}