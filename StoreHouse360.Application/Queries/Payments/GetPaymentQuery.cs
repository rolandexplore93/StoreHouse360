using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Payments
{
    public class GetPaymentQuery : IRequest<Payment>
    {
        public int Id { get; set; }
    }
    public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, Payment>
    {
        private readonly IPaymentRepository _paymentRepository;
        public GetPaymentQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        public Task<Payment> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
        {
            return _paymentRepository.FindByIdAsync(request.Id, new FindOptions { IncludeRelations = true });
        }
    }
}
