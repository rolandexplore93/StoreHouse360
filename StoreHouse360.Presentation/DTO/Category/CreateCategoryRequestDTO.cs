using StoreHouse360.Application.Commands.Categories;
using StoreHouse360.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.DTO.Category
{
    public class CreateCategoryRequestDTO : IMapFrom<CreateCategoryCommand>
    {
        //[Required] public int Id { get; set; }
        [Required] public string Name { get; set; }
    }
}
