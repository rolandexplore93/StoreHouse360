using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using StoreHouse360.DTO.Common;

namespace StoreHouse360.DTO.Warehouses
{
    public class WarehouseVM : IViewModel, IMapFrom<Warehouse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}