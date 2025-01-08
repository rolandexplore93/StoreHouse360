using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Payments
{
    public class GetAllPaymentsQuery : GetPaginatedQuery<Payment>
    {
        public int InvoiceId { get; set; } = 0;
        public PaymentType? PaymentType { get; set; } = default;
        public PaymentIoType? PaymentIoType { get; set; } = default;
    }
    public class GetAllPaymentsQueryHandler : PaginatedQueryHandler<GetAllPaymentsQuery, Payment>
    {
        private readonly IPaymentRepository _paymentRepository;
        public GetAllPaymentsQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        protected override async Task<IQueryable<Payment>> GetQuery(GetAllPaymentsQuery request, CancellationToken cancellationToken)
        {
            var payments = await _paymentRepository.GetAllAsync(new GetAllOptions<Payment> { IncludeRelations = true });
            var res = payments
                .Where(payment => payment.InvoiceId == request.InvoiceId || request.InvoiceId == default)
                .Where(payment => payment.PaymentType == request.PaymentType || request.PaymentType == default)
                .Where(payment => payment.PaymentIoType == request.PaymentIoType || request.PaymentIoType == default);
            return res;
        }
    }
}
