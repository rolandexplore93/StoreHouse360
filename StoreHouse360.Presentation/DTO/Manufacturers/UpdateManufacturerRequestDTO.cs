using StoreHouse360.Application.Commands.Manufacturers;
using StoreHouse360.Application.Common.Mappings;

namespace StoreHouse360.DTO.Manufacturers
{
    public class UpdateManufacturerRequestDTO : IMapFrom<UpdateManufacturerCommand>
    {
        public string Name { get; set; }
        public string? Code { get; set; }
    }
}
