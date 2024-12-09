using StoreHouse360.Authentication.DTO;

namespace StoreHouse360.Authentication.Services
{
    public interface IAuthenticationService
    {
        Task<JwtLoginResult> JwtLogin(JwtLoginRequest requestDTO);
    }
}