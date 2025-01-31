using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Settings;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api
{
    public class SettingsController : ApiControllerBase
    {
        private readonly AppSettings settings;
        public SettingsController(IMediator mediator, IMapper mapper, AppSettings settings) : base(mediator, mapper)
        {
            this.settings = settings;
        }

        [HttpGet]
        public ActionResult<BaseResponse<AppSettings>> Get()
        {
            return Ok(settings);
        }
    }
}
