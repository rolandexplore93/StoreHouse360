using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreHouse360.Infrastructure.Persistence.Database.Models
{
    [Table("Units")]
    public class UnitDb : IMapFrom<Unit>, IDatabaseModel
    {
        [Key] 
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
