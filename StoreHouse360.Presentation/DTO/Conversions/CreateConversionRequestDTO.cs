using StoreHouse360.Application.Commands.Conversions;
using StoreHouse360.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.DTO.Conversions
{
    public class CreateConversionRequestDTO : IMapFrom<CreateConversionCommand>
    {
        [Required]
        public int FromWarehouseId { get; set; }

        [Required]
        public int ToWarehouseId { get; set; }

        [Required]
        public int FromProductId { get; set; }

        [Required]
        public int ToProductId { get; set; }

        [Required]
        public int FromPlaceId { get; set; }

        [Required]
        public int ToPlaceId { get; set; }

        [Required]
        public int FromQuantity { get; set; }

        [Required]
        public int ToQuantity { get; set; }

        public string? Note { get; set; }

        public bool IgnoreMinLevelWarnings { get; set; } = false;
    }
}
