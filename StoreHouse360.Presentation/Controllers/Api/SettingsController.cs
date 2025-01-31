using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Settings;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api
{
    [Authorize]
    public class SettingsController : ApiControllerBase
    {
        public SettingsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
        
        [HttpGet]
        public ActionResult<BaseResponse<AppSettings>> Get([FromServices] AppSettings settings)
        {
            return Ok(settings);
        }
    }
}
