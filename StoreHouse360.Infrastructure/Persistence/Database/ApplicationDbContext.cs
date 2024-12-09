using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoreHouse360.Infrastructure.Persistence.Database.Models;

namespace StoreHouse360.Infrastructure.Persistence.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationIdentityUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<CategoryDb> Categories { get; set; }
        public DbSet<ManufacturerDb> Manufacturers { get; set; }
        public DbSet<UnitDb> Units { get; set; }
        public DbSet<CurrencyDb> Currencies { get; set; }
    }
}
