using MediatR;

namespace StoreHouse360.Application.Queries.Invoicing
{
    public class CheckProductQuantityQuery : IRequest<CheckProductQuantityResult>
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class CheckProductQuantityResult
    {

    }
}
