using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using StoreHouse360.DTO.Common;

namespace StoreHouse360.DTO.Manufacturers
{
    public class ManufacturerVM : IMapFrom<Manufacturer>, IViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; }
    }
}
