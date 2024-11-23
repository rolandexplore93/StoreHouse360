using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.DTO.Category
{
    public class CreateCategoryRequestDTO
    {
        [Required] public string Id { get; set; }
        [Required] public string Name { get; set; }
    }
}
