using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Commands.Currencies;
using StoreHouse360.Application.Queries.Currencies;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Currencies;
using StoreHouse360.DTO.Pagination;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api
{  
    //[Authorize]
    public class CurrenciesController : ApiControllerBase
    {
        public CurrenciesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<CurrencyVM>>> CreateCurrency(CreateCurrencyRequestDTO request)
        {
            var command = _mapper.Map<CreateCurrencyCommand>(request);
            var currencyId = await Mediator.Send(command);
            var currency = await Mediator.Send(new GetCurrencyQuery { Id = currencyId });
            return Ok(currency.ToViewModel<CurrencyVM>(_mapper));
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<PaginationVM<CurrencyVM>>>> GetAll([FromQuery] CurrenciesQueryParams request)
        {
            var currencies = await Mediator.Send(request.AsQuery<GetAllCurrenciesQuery>(_mapper));
            return Ok(currencies.ToViewModels<CurrencyVM>(_mapper));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<CurrencyVM>>> GetCurrency(int id)
        {
            var currencies = await Mediator.Send(new GetCurrencyQuery { Id = id });
            return Ok(currencies.ToViewModel<CurrencyVM>(_mapper));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<CurrencyVM>>> UpdateCurrency(UpdateCurrencyRequestDTO request, int id)
        {
            var command = _mapper.Map<UpdateCurrencyCommand>(request);
            command.Id = id;
            var resultId = await Mediator.Send(command);
            return await GetCurrency(resultId);
        }

        [HttpDelete("{id}")]
        public async Task DeleteCurrency(int id)
        {
            await Mediator.Send(new DeleteCurrencyCommand() { key = id });
        }
    }
}