using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.Infrastructure.Persistence.Database.Models
{
    public class PaymentDb : IMapFrom<Payment>, IDatabaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        public InvoiceDb? Invoice { get; set; }
        public string? Note { get; set; }
        public PaymentType PaymentType { get; set; }
        public PaymentIoType PaymentIoType { get; set; }
        public double Amount { get; set; }
        public int CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public CurrencyDb? Currency { get; set; }

        [ForeignKey("ObjectId")]
        public IEnumerable<CurrencyAmount> CurrencyAmounts { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
