using StoreHouse360.DTO.Users;

namespace StoreHouse360.DTO.Auth
{
    public class LoginResponseDTO
    {
        public string Token { get; init; }
        public UserVM User { get; init; }
    }
}