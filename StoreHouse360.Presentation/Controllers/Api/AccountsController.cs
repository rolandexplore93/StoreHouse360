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
            var id = await Mediator.Send(command);
            return await GetAccount(id);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<PaginationVM<AccountVM>>>> GetAccounts([FromQuery] AccountsQueryParams request)
        {
            var query = request.AsQuery<GetAllAccountsQuery>(_mapper);
            var accountEntities = await Mediator.Send(query);
            return Ok(accountEntities.ToViewModels<AccountVM>(_mapper));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<AccountVM>>> GetAccount(int id)
        {
            var query = new GetAccountQuery { Id = id };
            var accountEntity = await Mediator.Send(query);
            return Ok(accountEntity.ToViewModel<AccountVM>(_mapper), "mytest");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<AccountVM>>> UpdateAccount(UpdateAccountRequestDTO request, int id)
        {
            var command = _mapper.Map<UpdateAccountCommand>(request);
            command.Id = id;
            var updatedAccountId = await Mediator.Send(command);
            return await GetAccount(updatedAccountId);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAccount(int id)
        {
            await Mediator.Send(new DeleteAccountCommand { key = id });
        }
    }
}
