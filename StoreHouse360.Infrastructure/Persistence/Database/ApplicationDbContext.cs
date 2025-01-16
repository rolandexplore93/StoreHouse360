using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoreHouse360.Infrastructure.Persistence.Database.Models;
using StoreHouse360.Infrastructure.Persistence.Database.Models.Common;

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
        public DbSet<AppSettingDb> Settings { get; set; }
        public DbSet<ProductDb> Products { get; set; }
        public DbSet<WarehouseDb> Warehouses { get; set; }
        public DbSet<StoragePlaceDb> StoragePlaces { get; set; }
        public DbSet<AccountDb> Accounts { get; set; }
        public DbSet<CurrencyAmountDb> CurrencyAmounts { get; set; }
        public DbSet<InvoiceDb> Invoices { get; set; }
        public DbSet<ProductMovementDb> ProductMovements { get; set; }
        public DbSet<PaymentDb> Payments { get; set; }
        public DbSet<NotificationDb> Notifications { get; set; }
        public DbSet<CountryOriginDb> CountryOrigins { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyGlobalFilters<ISoftDelete>(s => !s.IsDeleted);
        }
    }
}
