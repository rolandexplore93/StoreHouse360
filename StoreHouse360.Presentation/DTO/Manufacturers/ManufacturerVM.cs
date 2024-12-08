using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using StoreHouse360.Dto.Common;

namespace StoreHouse360.Dto.Manufacturers
{
    public class ManufacturerVM : IMapFrom<Manufacturer>, IViewModel
    {
        public string Name { get; set; }
        public string? Code { get; set; }
    }
}
