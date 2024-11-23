using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.Presentation.DTO.Requests.Auth
{
    public class AuthRequestDTO
    {
        [Required] public string UserName { get; set; }
        [Required] public string Password { get; set; }
    }
}
