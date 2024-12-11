using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Commands.Accounts;
using StoreHouse360.Application.Queries.Accounts;
using StoreHouse360.DTO.Accounts;
using StoreHouse360.DTO.Common;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api
{
    //[Authorize]
    public class AccountController : ApiControllerBase
    {
        public AccountController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<AccountVM>>> CreateAccount(CreateAccountRequestDTO request)
        {
            var command = _mapper.Map<CreateAccountCommand>(request);
            var unitId = await Mediator.Send(command);
            return await GetAccount(unitId);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<AccountVM>>>> GetAccounts()
        {
            var query = new GetAllAccountsQuery();
            var unitEntities = await Mediator.Send(query);
            return Ok(unitEntities.ToViewModels<AccountVM>(_mapper));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<AccountVM>>> GetAccount(int id)
        {
            var query = new GetAccountQuery { Id = id };
            var unitEntity = await Mediator.Send(query);
            return Ok(unitEntity.ToViewModel<AccountVM>(_mapper));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<AccountVM>>> CreateAccount(UpdateAccountRequestDTO request,
            int id)
        {
            var command = _mapper.Map<UpdateAccountCommand>(request);
            command.Id = id;
            var updatedAccountId = await Mediator.Send(command);
            return await GetAccount(updatedAccountId);
        }
    }
}
