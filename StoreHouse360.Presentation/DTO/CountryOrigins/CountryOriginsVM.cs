using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using StoreHouse360.DTO.Common;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.DTO.CountryOrigins
{
    public class CountryOriginVM : IViewModel, IMapFrom<CountryOrigin>
    {
        public int Id { get; init; }
        [Required] public string Name { get; init; }
    }
}
