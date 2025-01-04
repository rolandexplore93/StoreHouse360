using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Commands.Invoicing;
using StoreHouse360.Application.Queries.Invoicing;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Invoices;
using StoreHouse360.DTO.Pagination;
using StoreHouse360.Presentation.DTO.Common.Responses;
using StatusCodes = StoreHouse360.Domain.Exceptions.StatusCodes;

namespace StoreHouse360.Controllers.Api
{
    //[Authorize]
    public class InvoicesController : ApiControllerBase
    {
        public InvoicesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {

        }

        [HttpPost]
        [ProducesResponseType(typeof(ActionResult<BaseResponse<InvoiceVM>>), 200)]
        [ProducesResponseType(typeof(ActionResult<BaseResponse<IList<int>>>), StatusCodes.ProductMinimumLevelExceededExceptionCode)]
        public async Task<ActionResult<BaseResponse<InvoiceVM>>> Create(CreateInvoiceRequestDTO request)
        {
            var invoiceId = await Mediator.Send(_mapper.Map<CreateInvoiceCommand>(request));
            var invoice = await Mediator.Send(new GetInvoiceQuery { Id = invoiceId });
            return Ok(invoice.ToViewModel<InvoiceVM>(_mapper));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<InvoiceVM>>> Get(int id)
        {
            var invoice = await Mediator.Send(new GetInvoiceQuery { Id = id });
            return Ok(invoice.ToViewModel<InvoiceVM>(_mapper));
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<InvoiceVM>>>> GetAll([FromQuery] InvoicesQueryParams requestQueryParams)
        {
            var query = _mapper.Map<GetAllInvoicesQuery>(requestQueryParams);
            var invoices = await Mediator.Send(query);
            return Ok(invoices.ToViewModels<InvoiceVM>(_mapper));
        }

        [HttpGet("{id}/items")]
        public async Task<ActionResult<BaseResponse<IEnumerable<ProductMovementVM>>>> GetInvoiceItems(int id)
        {
            var items = await Mediator.Send(new GetProductMovementsQuery { InvoiceId = id });
            return Ok(items.ToViewModels<ProductMovementVM>(_mapper));
        }
    }
}



