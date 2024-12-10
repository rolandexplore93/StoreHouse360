using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.Infrastructure.Persistence.Database.Models
{
    [Table("Products")]
    public class ProductDb : IMapFrom<Product>, IDatabaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public CategoryDb Category { get; set; }
        public int ManufacturerId { get; set; }
        [ForeignKey("ManufacturerId")]
        public ManufacturerDb Manufacturer { get; set; }
        public int UnitId { get; set; }
        [ForeignKey("UnitId")]
        public UnitDb Unit { get; set; }
        public string Barcode { get; set; }
        public double Price { get; set; }
        public int CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public CurrencyDb Currency { get; set; }
    }
}
