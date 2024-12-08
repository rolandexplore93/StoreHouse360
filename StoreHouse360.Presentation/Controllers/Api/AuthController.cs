using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Authentication.DTO;
using StoreHouse360.Authentication.Services;
using StoreHouse360.Dto.Common;
using StoreHouse360.Dto.Users;
using StoreHouse360.Presentation.DTO.Auth;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api
{
    public class AuthController : ApiControllerBase
    {
        //private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IMediator mediator, IMapper mapper, IAuthenticationService authenticationService) : base(mediator, mapper)
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<BaseResponse<string>>> Login(LoginRequestDTO requestDTO)
        {
            var result = await _authenticationService.JwtLogin(_mapper.Map<JwtLoginRequest>(requestDTO));

            return Ok(new LoginResponseDTO()
            {
                User = result.User.ToViewModel<UserVM>(_mapper),
                Token = result.Token
            });
        }
    }
}