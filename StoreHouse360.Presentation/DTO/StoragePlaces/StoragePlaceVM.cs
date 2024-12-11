using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using StoreHouse360.DTO.Common;

namespace StoreHouse360.DTO.StoragePlaces
{
    public class StoragePlaceVM : IViewModel, IMapFrom<StoragePlace>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Warehouse? Warehouse { get; set; }
        public StoragePlace? Container { get; set; }
        public bool HasContainer { get; set; }
    }
}
