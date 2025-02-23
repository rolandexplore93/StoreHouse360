using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Commands.Roles;
using StoreHouse360.Application.Queries.Roles;
using StoreHouse360.Controllers.Api;
using StoreHouse360.DTO.Authorization.Roles;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Pagination;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api.Authorization
{
    [Route("api/authorization/[controller]")]
    public class RolesController : ApiControllerBase
    {
        public RolesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<RoleVM>>> Create(CreateRoleRequestDTO request)
        {
            var command = _mapper.Map<CreateRoleRequestDTO, CreateRoleCommand>(request);

            var roleId = await Mediator.Send(command);

            var role = await Mediator.Send(new GetRoleQuery { Id = roleId });

            return Ok(role.ToViewModel<RoleVM>(_mapper));
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<PaginationVM<RoleVM>>>> GetRoles()
        {
            var roles = await Mediator.Send(new GetAllRolesQuery());

            return Ok(roles.ToViewModel<RoleVM>(_mapper));
        }

        [HttpDelete("{id}")]
        public Task Delete(int id)
        {
            return Mediator.Send(new DeleteRoleCommand { Id = id });
        }
    }
}
