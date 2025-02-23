using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Application.Common.Security;

namespace StoreHouse360.Infrastructure.Persistence.Database.Models
{
    public class AppRole : IdentityRole<int>, IMapFrom<Role>
    {
        public string Permissions {  get; set; }

        public void Map(Profile profile)
        {
            profile.CreateMap<string, Permissions>().ConvertUsing(s => Application.Common.Security.Permissions.From(s));
            profile.CreateMap<Permissions, string>().ConvertUsing(p => p.ToString());
            profile.CreateMap<Role, AppRole>();
            profile.CreateMap<AppRole, Role>();
        }
    }
}
