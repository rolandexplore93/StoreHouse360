using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Commands.StoragePlaces;
using StoreHouse360.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.DTO.StoragePlaces
{
    public class UpdateStoragePlaceRequestDTO : IMapFrom<UpdateStoragePlaceCommand>
    {
        //[FromRoute(Name = "id")]
        //public int Id { get; set; }

        //[FromRoute(Name = "warehouseId")]
        //[Required]
        //public int WarehouseId { get; set; }

        [Required]
        public string Name { get; set; }
        public int? ContainerId { get; set; }
    }
}
