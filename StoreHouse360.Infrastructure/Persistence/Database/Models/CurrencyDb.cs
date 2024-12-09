using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.Infrastructure.Persistence.Database.Models
{
    [Table("Currencies")]
    public class CurrencyDb : IMapFrom<Currency>, IDatabaseModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public float Factor { get; set; }
    }
}
