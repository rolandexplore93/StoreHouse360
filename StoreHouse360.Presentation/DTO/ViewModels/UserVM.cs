using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.Presentation.DTO.ViewModels
{
    public class UserVM
    {
        [Required] public string UserName { get; set; }
    }
}
