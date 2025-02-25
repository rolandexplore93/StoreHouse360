using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Commands.Conversions;
using StoreHouse360.Application.Queries.Conversions;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Conversions;
using StoreHouse360.DTO.Pagination;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api
{
    [Authorize]
    public class ConversionsController : ApiControllerBase
    {
        public ConversionsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<PaginationVM<ConversionVM>>>> GetAll()
        {
            var getAllQuery = new GetAllConversionsQuery();

            var conversionsPage = await Mediator.Send(getAllQuery);

            return Ok(conversionsPage.ToViewModel<ConversionVM>(_mapper));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BaseResponse<ConversionVM>>> Get(int id)
        {
            var conversionsPage = await Mediator.Send(new GetAllConversionsQuery());

            var conversion = conversionsPage.First(conversion => conversion.Id == id);

            return Ok(conversion.ToViewModel<ConversionVM>(_mapper));
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<ConversionVM>>> Create(CreateConversionRequestDTO request)
        {
            var createInvoiceCommand = _mapper.Map<CreateConversionCommand>(request);
            var conversionId = await Mediator.Send(createInvoiceCommand);

            var conversionsPage = await Mediator.Send(new GetAllConversionsQuery());
            var conversion = conversionsPage.First(conversion => conversion.Id == conversionId);
            return Ok(conversion.ToViewModel<ConversionVM>(_mapper));
        }
    }
}
