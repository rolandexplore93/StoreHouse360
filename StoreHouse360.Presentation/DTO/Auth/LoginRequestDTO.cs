using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Authentication.DTO;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.DTO.Auth
{
    public class LoginRequestDTO : IMapFrom<JwtLoginRequest>
    {
        [Required] public string UserName { get; set; }
        [Required] public string Password { get; set; }
    }
}
