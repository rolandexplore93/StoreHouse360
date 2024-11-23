using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Authentication.DTO
{
    public class JwtLoginResult
    {
        public string Token { get; init; }
        public User User { get; init; }
    }
}