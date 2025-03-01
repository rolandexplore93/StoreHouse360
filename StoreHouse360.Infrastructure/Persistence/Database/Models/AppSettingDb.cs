using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.Infrastructure.Persistence.Database.Models
{
    [Table("Settings")]
    public class AppSettingDb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
