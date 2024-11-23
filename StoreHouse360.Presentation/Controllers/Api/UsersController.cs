using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Commands.Users.CreateUser;
using StoreHouse360.Application.Queries.Users;
using StoreHouse360.Domain.Entities;
using StoreHouse360.DTO.Users;
namespace StoreHouse360.Controllers.Api
{
    public class UsersController : ApiControllerBase
    {
        private ILogger<UsersController> _logger;
        private IMapper _mapper;
        public UsersController(ILogger<UsersController> logger, IMapper mapper, IMediator mediator) : base(mediator)
        {
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(CreateUserRequestDTO requestDTO)
        {
            var command = _mapper.Map<CreateUserCommand>(requestDTO);
            var userId = await Mediator.Send(command);

            var user = await Mediator.Send(new GetUserQuery { Id = userId });

            return Ok(user);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var result = await Mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }
    }
}
