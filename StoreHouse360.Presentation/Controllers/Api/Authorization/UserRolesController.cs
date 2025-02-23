using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Commands.UserRoles;
using StoreHouse360.Application.Queries.UserRoles;
using StoreHouse360.DTO.Authorization.Roles;
using StoreHouse360.DTO.Authorization.UserRoles;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Pagination;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api.Authorization
{
    [Route("api/authorization/[controller]")]
    public class UserRolesController : ApiControllerBase
    {
        public UserRolesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<BaseResponse<PaginationVM<RoleVM>>>> GetUserRoles(int userId)
        {
            var roles = await Mediator.Send(new GetUserRolesQuery { UserId = userId });

            return Ok(roles.ToViewModel<RoleVM>(_mapper));
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult<BaseResponse<PaginationVM<RoleVM>>>> UpdateUserRoles(int userId, UpdateUserRolesRequestDTO request)
        {
            var command = new UpdateUserRolesCommand { UserId = userId, RoleIds = request.RoleIds };

            await Mediator.Send(command);

            return await GetUserRoles(userId);
        }
    }
}
