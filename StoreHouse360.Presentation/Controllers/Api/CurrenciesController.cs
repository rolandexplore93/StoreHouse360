using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Commands.Currencies;
using StoreHouse360.Application.Queries.Currencies;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Currencies;
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
        public async Task<ActionResult<BaseResponse<IEnumerable<CurrencyVM>>>> GetAll()
        {
            var currencies = await Mediator.Send(new GetAllCurrenciesQuery());
            return Ok(currencies.ToViewModels<CurrencyVM>(_mapper));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<CurrencyVM>>> GetCurrency(int id)
        {
            var currencies = await Mediator.Send(new GetCurrencyQuery { Id = id });
            return Ok(currencies.ToViewModel<CurrencyVM>(_mapper));
        }
    }
}