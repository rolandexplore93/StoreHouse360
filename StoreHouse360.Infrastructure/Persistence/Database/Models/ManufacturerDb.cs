using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreHouse360.Infrastructure.Persistence.Database.Models
{
    [Table("Manufacturers")]
    public class ManufacturerDb : IMapFrom<Manufacturer>, IDatabaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Code { get; set; }
    }
}