using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Commands.StoragePlaces;
using StoreHouse360.Application.Common.Mappings;

namespace StoreHouse360.DTO.StoragePlaces
{
    public class CreateStoragePlaceRequestDTO : IMapFrom<CreateStoragePlaceCommand>
    {
        //[FromRoute(Name = "warehouseId")]
        //public int WarehouseId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? ContainerId { get; set; }
    }
}
