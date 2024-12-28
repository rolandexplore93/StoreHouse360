using StoreHouse360.Application.Commands.Accounts;
using StoreHouse360.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.DTO.Accounts
{
    public class UpdateAccountRequestDTO : IMapFrom<UpdateAccountCommand>
    {
        [Required] public string Code { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Phone { get; set; }
        [Required] public string City { get; set; }
    }
}
