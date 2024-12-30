using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;
using StoreHouse360.Domain.Exceptions;
using Unit = MediatR.Unit;

namespace StoreHouse360.Application.Queries.Payments
{
    public class CheckInvoicePaymentQuery : IRequest<Unit>
    {
        public int InvoiceId { get; set; }
        public double Amount { get; set; }
    }
    public class CheckInvoicePaymentQueryHandler : IRequestHandler<CheckInvoicePaymentQuery, Unit>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        public CheckInvoicePaymentQueryHandler(IPaymentRepository paymentRepository, IInvoiceRepository invoiceRepository)
        {
            _paymentRepository = paymentRepository;
            _invoiceRepository = invoiceRepository;
        }
        public async Task<Unit> Handle(CheckInvoicePaymentQuery request, CancellationToken cancellationToken)
        {
            var payments = await _paymentRepository.GetAllAsync();
            double paymentsSum = payments.Where(payment => payment.Id == request.InvoiceId).Sum(payment => payment.Amount);

            Invoice invoice = await _invoiceRepository.FindByIdAsync(request.InvoiceId);

            if (request.Amount + paymentsSum > invoice.TotalPrice)
            {
                throw new OverPaymentException();
            }

            return Unit.Value;
        }

    }
}
