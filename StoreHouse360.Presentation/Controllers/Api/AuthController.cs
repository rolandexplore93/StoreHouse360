using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Authentication.DTO;
using StoreHouse360.Authentication.Services;
using StoreHouse360.Presentation.DTO.Auth;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api
{
    public class AuthController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IMediator mediator, IMapper mapper, IAuthenticationService authenticationService) : base(mediator, mapper)
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        //[HttpPost("auth")]
        //public ActionResult<BaseResponse<AuthenticatedUserVM>> Authenticate(AuthRequestDTO requestDTO)
        //{
        //    var authenticatedUser = new AuthenticatedUserVM()
        //    {
        //        Token = "zxidfgowjeiouqhwhfgcnxowuehfujeienwipejyfiowwniiuqiwu",
        //        UserVM = new UserVM()
        //        {
        //            UserName = requestDTO.UserName
        //        }
        //    };

        //    return Ok(authenticatedUser, "Successfully authenticated...");
        //}

        [HttpPost("login")]
        public async Task<ActionResult<BaseResponse<string>>> Login(LoginRequestDTO requestDTO)
        {
            var result = await _authenticationService.JwtLogin(_mapper.Map<JwtLoginRequest>(requestDTO));

            return Ok(new LoginResponseDTO()
            {
                User = result.User,
                Token = result.Token
            });
        }
    }
}