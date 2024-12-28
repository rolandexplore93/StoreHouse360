using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Commands.Accounts;
using StoreHouse360.Application.Queries.Accounts;
using StoreHouse360.DTO.Accounts;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Pagination;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api
{
    //[Authorize]
    public class AccountsController : ApiControllerBase
    {
        public AccountsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<AccountVM>>> CreateAccount(CreateAccountRequestDTO request)
        {
            var command = _mapper.Map<CreateAccountCommand>(request);
            var accountId = await Mediator.Send(command);
            return await GetAccount(accountId);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<PaginationVM<AccountVM>>>> GetAccounts([FromQuery] PaginationRequestParams request)
        {
            //var query = new GetAllAccountsQuery();
            var query = request.AsQuery(new GetAllAccountsQuery());
            var accountEntities = await Mediator.Send(query);
            return Ok(accountEntities.ToViewModels<AccountVM>(_mapper));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<AccountVM>>> GetAccount(int id)
        {
            var query = new GetAccountQuery { Id = id };
            var accountEntity = await Mediator.Send(query);
            return Ok(accountEntity.ToViewModel<AccountVM>(_mapper));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<AccountVM>>> CreateAccount(UpdateAccountRequestDTO request, int id)
        {
            var command = _mapper.Map<UpdateAccountCommand>(request);
            command.Id = id;
            var updatedAccountId = await Mediator.Send(command);
            return await GetAccount(updatedAccountId);
        }
    }
}
