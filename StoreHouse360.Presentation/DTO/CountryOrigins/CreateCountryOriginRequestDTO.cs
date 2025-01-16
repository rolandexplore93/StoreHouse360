using StoreHouse360.Application.Commands.CountryOrigins;
using StoreHouse360.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.DTO.CountryOrigins
{
    public class CreateCountryOriginRequest : IMapFrom<CreateCountryOriginCommand>
    {
        [Required] public string Name { get; set; }
    }
}
