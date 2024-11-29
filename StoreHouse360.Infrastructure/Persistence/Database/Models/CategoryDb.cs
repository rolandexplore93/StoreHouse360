using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreHouse360.Infrastructure.Persistence.Database.Models
{
    [Table("Categories")]
    public class CategoryDb : IMapFrom<Category>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}