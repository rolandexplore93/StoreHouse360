//using StoreHouse360.Domain.Entities;
using StoreHouse360.Dto.Users;

namespace StoreHouse360.Presentation.DTO.Auth
{
    public class LoginResponseDTO
    {
        public string Token { get; init; }
        public UserVM User { get; init; }
    }
}