using StoreHouse360.Application.Commands.Categories;
using StoreHouse360.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.Dto.Category
{
    public class UpdateCategoryRequestDTO : IMapFrom<UpdateCategoryCommand>
    {
        [Required] public string Name { get; init; }
    }
}
