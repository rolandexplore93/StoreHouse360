using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Commands.Users;
using StoreHouse360.Application.Queries.Users;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Pagination;
using StoreHouse360.DTO.Users;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api
{
    public class UsersController : ApiControllerBase
    {
        private ILogger<UsersController> _logger;
        public UsersController(ILogger<UsersController> logger, IMapper mapper, IMediator mediator) : base(mediator, mapper)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<UserVM>>> CreateUser(CreateUserRequestDTO requestDTO)
        {
            var command = _mapper.Map<CreateUserCommand>(requestDTO);
            var userId = await Mediator.Send(command);

            var user = await Mediator.Send(new GetUserQuery { Id = userId });

            //return Ok(user);
            return Ok(user.ToViewModel<UserVM>(_mapper));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<BaseResponse<PaginationVM<UserVM>>>> GetUsers([FromQuery] PaginationRequestParams request)
        {
            var result = await Mediator.Send(request.AsQuery(new GetAllUsersQuery()));
            //return Ok(result);
            return Ok(result.ToViewModels<UserVM>(_mapper));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<UserVM>>> GetUser(int id)
        {
            var user = await Mediator.Send(new GetUserQuery { Id = id });
            return Ok(user.ToViewModel<UserVM>(_mapper));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<object?>>> DeleteUser(int id)
        {
            await Mediator.Send(new DeleteUserCommand { Id = id });
            return Ok("Deleted successfully");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<UserVM>>> UpdateUser(int id, UpdateUserRequestDTO request)
        {
            var command = _mapper.Map<UpdateUserCommand>(request);
            command.Id = id;
            await Mediator.Send(command);
            var user = await Mediator.Send(new GetUserQuery { Id = id });
            return Ok(user.ToViewModel<UserVM>(_mapper));
        }
    }
}
