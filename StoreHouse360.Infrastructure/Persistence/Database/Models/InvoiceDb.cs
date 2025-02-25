using Microsoft.EntityFrameworkCore.Metadata;
using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreHouse360.Infrastructure.Persistence.Database.Models
{
    public class InvoiceDb : IMapFrom<Invoice>, IDatabaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? AccountId { get; set; }
        [ForeignKey("AccountId")]
        public AccountDb? Account { get; set; }
        public int WarehouseId { get; set; }
        [ForeignKey("WarehouseId")]
        public WarehouseDb Warehouse { get; set; }

        public int? CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public CurrencyDb? Currency { get; set; }
        public double TotalPrice { get; set; }
        public IEnumerable<ProductMovementDb> Items { get; set; }
        public string? Note { get; set; }

        public DateTime CreatedAt { get; set; }

        public InvoiceStatus Status { get; set; }

        public InvoiceType Type { get; set; }
        public InvoiceAccountType AccountType { get; set; }
    }
}
