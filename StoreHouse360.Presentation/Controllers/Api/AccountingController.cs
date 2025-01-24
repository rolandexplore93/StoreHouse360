using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Queries.Accounting;
using StoreHouse360.DTO.Accounting;
using StoreHouse360.DTO.Common;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api
{
    public class AccountingController : ApiControllerBase
    {
        public AccountingController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpGet("accountStatement")]
        public async Task<ActionResult<BaseResponse<AccountStatementVM>>> AccountStatement([FromQuery] int accountId)
        {
            var accountStatement = await Mediator.Send(new GetAccountStatementQuery(accountId));
            var viewmodel = accountStatement.ToViewModel<AccountStatementVM>(_mapper);
            return Ok(viewmodel);
        }
    }
}
