using StoreHouse360.Application.Commands.Manufacturers;
using StoreHouse360.Application.Common.Mappings;

namespace StoreHouse360.Dto.Manufacturers
{
    public class CreateManufacturerRequestDTO : IMapFrom<CreateManufacturerCommand>
    {
        public string Name { get; set; }
        public string? Code { get; set; }
    }
}
