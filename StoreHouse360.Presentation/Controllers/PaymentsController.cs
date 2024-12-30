using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Queries.Payments;
using StoreHouse360.Controllers.Api;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Pagination;
using StoreHouse360.DTO.Payments;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers
{
    public class PaymentsController : ApiControllerBase
    {
        public PaymentsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<PaymentVM>>> Get(int id)
        {
            var payment = await Mediator.Send(new GetPaymentQuery { Id = id });
            return Ok(payment.ToViewModel<PaymentVM>(_mapper));
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<PaymentVM>>>> GetAll(
            [FromQuery] PaginationRequestParams request)
        {
            var query = request.AsQuery<GetAllPaymentsQuery>();
            var payments = await Mediator.Send(query);
            return Ok(payments.ToViewModels<PaymentVM>(_mapper));
        }
    }
}
