using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Presentation.DTO.Responses.Common;

namespace StoreHouse360.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiControllerBase : ControllerBase
    {
        protected readonly IMediator Mediator;

        public ApiControllerBase(IMediator mediator)
        {
            Mediator = mediator;
        }
        [NonAction]
        public override OkObjectResult Ok(object? value)
        {
            return Ok(value, "Success");
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
