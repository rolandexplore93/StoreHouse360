using StoreHouse360.Application.Commands.Users;
using StoreHouse360.Application.Common.Mappings;

namespace StoreHouse360.DTO.Users
{
    public class UpdateUserRequestDTO : IMapFrom<UpdateUserCommand>
    {
        //public int Id { get; set; }
        public string UserName { get; set; }
    }
}
