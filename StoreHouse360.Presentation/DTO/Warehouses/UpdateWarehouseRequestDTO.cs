using StoreHouse360.Application.Commands.Warehouses;
using StoreHouse360.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.DTO.Warehouses
{
    public class UpdateWarehouseRequestDTO : IMapFrom<UpdateWarehouseCommand>
    {
        [Required] public string Name { get; set; }

        [Required] public string Location { get; set; }
    }
}
