using StoreHouse360.Application.Commands.Units;
using StoreHouse360.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.Dto.Units
{
    public class CreateUnitRequestDTO : IMapFrom<CreateUnitCommand>
    {
        [Required] public string Name { get; set; }
        [Required] public int Value { get; set; }
    }
}
