using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoreHouse360.Application.Services.Events;
using StoreHouse360.Domain.Events;
using StoreHouse360.Infrastructure.Persistence.Database.Models;
using StoreHouse360.Infrastructure.Persistence.Database.Models.Common;

namespace StoreHouse360.Infrastructure.Persistence.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationIdentityUser, AppRole, int>
    {
        private readonly IEventPublisherService _eventPublisher;

        public ApplicationDbContext(DbContextOptions options, IEventPublisherService eventPublisher) : base(options)
        {
            _eventPublisher = eventPublisher;
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
        public DbSet<JournalDb> Journals { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyGlobalFilters<ISoftDelete>(s => !s.IsDeleted);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var events = ChangeTracker.Entries<IHasDomainEvents>()
                .Select(x => x.Entity.Events)
                .SelectMany(x => x)
                .Where(domainEvent => !domainEvent.IsPublished)
                .ToArray();
            var result = await base.SaveChangesAsync(cancellationToken);
            await DispatchEvents(events);
            return result;
        }
        private async Task DispatchEvents(DomainEvent[] events)
        {
            foreach (var @event in events)
            {
                @event.IsPublished = true;
                await _eventPublisher.Publish(@event);
            }
        }
    }
}
