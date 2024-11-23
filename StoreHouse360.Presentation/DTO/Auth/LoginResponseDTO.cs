using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Presentation.DTO.Auth
{
    public class LoginResponseDTO
    {
        public string Token { get; init; }

        public User User { get; init; }
    }
}