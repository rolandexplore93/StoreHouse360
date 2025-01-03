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
        public async Task<ActionResult<BaseResponse<PaymentVM>>> CreateIn(CreatePaymentRequestDTO request)
        {
            CreatePaymentCommand createPaymentCommand = _mapper.Map<CreatePaymentCommand>(request);
            createPaymentCommand.PaymentType = PaymentType.Normal;
            createPaymentCommand.PaymentIoType = PaymentIoType.In;
            var paymentId = await Mediator.Send(createPaymentCommand);
            var payment = await Mediator.Send(new GetPaymentQuery { Id = paymentId });
            return Ok(payment.ToViewModel<PaymentVM>(_mapper));
        }

        [HttpPost("out/")]
        public async Task<ActionResult<BaseResponse<PaymentVM>>> CreateOut(CreatePaymentRequestDTO request)
        {
            CreatePaymentCommand createPaymentCommand = _mapper.Map<CreatePaymentCommand>(request);
            createPaymentCommand.PaymentType = PaymentType.Normal;
            createPaymentCommand.PaymentIoType = PaymentIoType.Out;
            var paymentId = await Mediator.Send(createPaymentCommand);
            var payment = await Mediator.Send(new GetPaymentQuery { Id = paymentId });
            return Ok(payment.ToViewModel<PaymentVM>(_mapper));
        }
    }
}
