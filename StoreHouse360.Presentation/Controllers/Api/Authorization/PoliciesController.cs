using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.DTO.Authorization.Policies;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api.Authorization
{
    [Route("api/authorization/[controller]")]
    public class PoliciesController : ApiControllerBase
    {
        public PoliciesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpGet]
        public ActionResult<BaseResponse<GetPoliciesResponse>> GetAll()
        {
            return Ok(
                new GetPoliciesResponse()
                {
                    Methods = Enum.GetValues<Method>(),
                    Resources = Enum.GetValues<Resource>(),
                });
        }
    }
}
