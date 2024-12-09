using StoreHouse360.Application.Commands.Units;
using StoreHouse360.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.Dto.Units
{
    public class UpdateUnitRequestDTO : IMapFrom<UpdateUnitCommand>
    {
        [Required] public string Name { get; init; }
        [Required] public int Value { get; init; }
    }
}
