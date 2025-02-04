using Microsoft.AspNetCore.Identity;

namespace StoreHouse360.Infrastructure.Persistence.Database.Models
{
    public class AppRole : IdentityRole<int>
    {
        public string Permissions {  get; set; }
    }
}
