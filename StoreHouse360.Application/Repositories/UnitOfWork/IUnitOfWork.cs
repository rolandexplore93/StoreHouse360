﻿using StoreHouse360.Application.Repositories.Aggregates;

namespace StoreHouse360.Application.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IAccountRepository AccountRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public ICurrencyAmountRepository CurrencyAmountRepository { get; }
        public ICurrencyRepository CurrencyRepository { get; }
        public IInvoiceRepository InvoiceRepository { get; }
        public IManufacturerRepository ManufacturerRepository { get; }
        public IProductMovementRepository ProductMovementRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IStoragePlaceRepository StoragePlaceRepository { get; }
        public IUnitRepository UnitRepository { get; }
        public IUserRepository UserRepository { get; }
        public IWarehouseRepository WarehouseRepository { get; }
        public IPaymentRepository PaymentRepository { get; }
        public IInvoicePaymentsRepository InvoicePaymentsRepository { get; }
        public IJournalRepository JournalRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IUserRolesRepository UserRolesRepository { get; }
        public IConversionRepository ConversionRepository { get; }
        public void Commit();
        public Task CommitAsync();
    }
}
