using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.DTO.ViewModels
{
    public class CategoryVM
    {
        [Required] public string Id { get; set; }
        [Required] public string Name { get; set; }
    }
}
