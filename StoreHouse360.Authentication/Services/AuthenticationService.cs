using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StoreHouse360.Application.Exceptions;
using StoreHouse360.Application.Services.Identity;
using StoreHouse360.Authentication.DTO;
using StoreHouse360.Authentication.Options;
using StoreHouse360.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StoreHouse360.Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IIdentityService _identityService;
        private readonly JwtOptions _jwtOptions;
        private readonly ILogger<AuthenticationService> _logger;
        public AuthenticationService(IIdentityService identityService, IOptions<JwtOptions> jwtOptions, ILogger<AuthenticationService> logger)
        {
           _identityService = identityService;
           _jwtOptions = jwtOptions.Value;
           _logger = logger;
        }
        public async Task<JwtLoginResult> JwtLogin(JwtLoginRequest requestDTO)
        {
            var user = await FindUserByName(requestDTO.Username);
            var validPassword = await _identityService.CheckPasswordAsync(user, requestDTO.Password);

            if (!validPassword) throw new InvalidCredentialException();

            var token = GenerateToken(user);

            return new JwtLoginResult()
            {
                Token = token,
                User = user
            };
        }

        private async Task<User> FindUserByName(string username)
        {
            try
            {
                var user = await _identityService.FindUserByNameAsync(username);
                return user;
            }
            catch (NotFoundException ex)
            {
                throw new InvalidCredentialException();
            }
        }

        private string GenerateToken(User user)
        {
            var secretKey = System.Text.Encoding.ASCII.GetBytes(_jwtOptions.IssuerSigningKey);
            var expireAt = _jwtOptions.ExpirationDate;
            var jwtToken = new JwtSecurityToken(issuer: _jwtOptions.ValidIssuer,
                audience: _jwtOptions.ValidAudience,
                claims: Claims(user),
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(expireAt).DateTime,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
        private IEnumerable<Claim> Claims(User user) =>
            new[]
            {
            new Claim("Id", user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
    }
}
