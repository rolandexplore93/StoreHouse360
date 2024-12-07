using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using System.Text.Json.Serialization;

namespace StoreHouse360.Dto.Users
{
    public class UserVM : IMapFrom<User>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [JsonIgnore]
        public string? PasswordHash { get; set; }
    }
}
