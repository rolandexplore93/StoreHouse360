using Microsoft.EntityFrameworkCore.Metadata;
using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.Infrastructure.Persistence.Database.Models
{
    [Table("StoragePlaces")]
    public class StoragePlaceDb : IMapFrom<StoragePlace>, IDatabaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int WarehouseId { get; set; }
        [ForeignKey("WarehouseId")]
        public WarehouseDb? Warehouse { get; set; }
        public int? ContainerId { get; set; }
        [ForeignKey("ContainerId")]
        public StoragePlaceDb? Container { get; set; }
    }
}