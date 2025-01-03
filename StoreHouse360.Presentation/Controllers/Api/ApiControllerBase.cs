using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        protected IMediator Mediator;
        protected IMapper _mapper;

        public ApiControllerBase(IMediator mediator, IMapper mapper)
        {
            Mediator = mediator;
            _mapper = mapper;
        }

        [NonAction]
        public override OkObjectResult Ok(object? value)
        {
            return Ok(value, "message");
        }

        [NonAction]
        public OkObjectResult Ok(object? value, string message)
        {
            if (value == null)
            {
                return base.Ok(new NoDataResponse());
            }
            else
            {
                return base.Ok(
                    new BaseResponse<object>(
                        new ResponseMetaData() { Message = message },
                        data: value
                    )
                );
            }
        }
    }
}
