using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreHouse360.Infrastructure.Persistence.Database.Models
{
    [Table("ProductMovements")]
    public class ProductMovementDb : IMapFrom<ProductMovement>, IDatabaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        public InvoiceDb Invoice { get; set; }
        public int? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public ProductDb? Product { get; set; }

        public int? PlaceId { get; set; }
        [ForeignKey("PlaceId")]
        public StoragePlaceDb? Place { get; set; }

        public int Quantity { get; set; }

        public double UnitPrice { get; set; }

        public double TotalPrice { get; set; }

        public int? CurrencyId { get; set; }

        [ForeignKey("CurrencyId")]
        public CurrencyDb? Currency { get; set; }
        public IEnumerable<CurrencyAmountDb>? CurrencyAmounts { get; set; }
        public ProductMovementType Type { get; set; }

        public string? Note { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
