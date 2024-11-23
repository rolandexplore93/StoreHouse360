using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Presentation.DTO.Requests.Auth;
using StoreHouse360.Presentation.DTO.Responses.Common;
using StoreHouse360.Presentation.DTO.ViewModels;

namespace StoreHouse360.Presentation.Controllers
{
    public class AuthController : ApiControllerBase
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("auth")]
        public ActionResult<BaseResponse<AuthenticatedUserVM>> Authenticate(AuthRequestDTO requestDTO)
        {
            var authenticatedUser = new AuthenticatedUserVM()
            {
                Token = "zxidfgowjeiouqhwhfgcnxowuehfujeienwipejyfiowwniiuqiwu",
                UserVM = new UserVM()
                {
                    UserName = requestDTO.UserName
                }
            };

            return Ok(authenticatedUser, "Successfully authenticated...");
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok(new { Name = "Roland C#", Profession = "Software Developer" });
        }


    }
}