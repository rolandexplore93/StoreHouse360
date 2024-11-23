using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.Presentation.DTO.ViewModels
{
    public class AuthenticatedUserVM
    {
        [Required] public string Token { get; set; }
        [Required] public UserVM UserVM { get; set; }
    }
}