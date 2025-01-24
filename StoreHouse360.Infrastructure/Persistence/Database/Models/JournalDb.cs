using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.Infrastructure.Persistence.Database.Models
{
    public class JournalDb : IMapFrom<Journal>, IDatabaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SourceAccountId { get; set; }
        [ForeignKey("SourceAccountId")]
        public AccountDb? SourceAccount { get; set; }
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public AccountDb? Account { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public int CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public CurrencyDb? Currency { get; set; }
    }
}
