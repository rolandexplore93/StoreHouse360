using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Commands.Payments;
using StoreHouse360.Application.Queries.Payments;
using StoreHouse360.Domain.Entities;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Pagination;
using StoreHouse360.DTO.Payments;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api
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
        public async Task<ActionResult<BaseResponse<IEnumerable<PaymentVM>>>> GetAll([FromQuery] GetAllPaymentsRequestDTO request)
        {
            var query = _mapper.Map<GetAllPaymentsQuery>(request);
            var payments = await Mediator.Send(query);
            return Ok(payments.ToViewModels<PaymentVM>(_mapper));
        }

        [HttpPost("in/")]
        public Task<ActionResult<BaseResponse<PaymentVM>>> CreateIn(CreatePaymentRequestDTO request)
        {
            return CreatePayment(request, PaymentType.Normal, PaymentIoType.In);
        }

        [HttpPost("in/discount/")]
        public Task<ActionResult<BaseResponse<PaymentVM>>> CreateDiscountIn(CreatePaymentRequestDTO request)
        {
            return CreatePayment(request, PaymentType.Discount, PaymentIoType.In);
        }

        [HttpPost("out/")]
        public Task<ActionResult<BaseResponse<PaymentVM>>> CreateOut(CreatePaymentRequestDTO request)
        {
            return CreatePayment(request, PaymentType.Normal, PaymentIoType.Out);
        }

        [HttpPost("out/discount/")]
        public Task<ActionResult<BaseResponse<PaymentVM>>> CreateDiscountOut(CreatePaymentRequestDTO request)
        {
            return CreatePayment(request, PaymentType.Discount, PaymentIoType.Out);
        }
        private async Task<ActionResult<BaseResponse<PaymentVM>>> CreatePayment(CreatePaymentRequestDTO request, PaymentType paymentType, PaymentIoType paymentIoType)
        {
            CreatePaymentCommand createPaymentCommand = _mapper.Map<CreatePaymentCommand>(request);
            createPaymentCommand.PaymentType = paymentType;
            createPaymentCommand.PaymentIoType = paymentIoType;
            var paymentId = await Mediator.Send(createPaymentCommand);
            var payment = await Mediator.Send(new GetPaymentQuery { Id = paymentId });
            return Ok(payment.ToViewModel<PaymentVM>(_mapper));
        }
    }
}
