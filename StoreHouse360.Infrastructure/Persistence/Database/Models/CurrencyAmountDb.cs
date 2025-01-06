using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreHouse360.Infrastructure.Persistence.Database.Models
{
    [Table("CurrencyAmounts")]
    public class CurrencyAmountDb : IMapFrom<CurrencyAmount>, IDatabaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public CurrencyAmountKey Key { get; set; }
        public double Amount { get; set; }
        public int CurrencyId { get; set; }
        public CurrencyDb Currency { get; set; }
    }
}