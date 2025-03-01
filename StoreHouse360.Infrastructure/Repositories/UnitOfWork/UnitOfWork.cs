﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Application.Repositories.Aggregates;
using StoreHouse360.Application.Repositories.UnitOfWork;
using StoreHouse360.Infrastructure.Persistence.Database;
using StoreHouse360.Infrastructure.Repositories.Aggregates;

namespace StoreHouse360.Infrastructure.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private readonly IDbContextTransaction _transaction;
        private readonly Lazy<IAccountRepository> _accountRepository;
        private readonly Lazy<ICategoryRepository> _categoryRepository;
        private readonly Lazy<ICurrencyAmountRepository> _currencyAmountRepository;
        private readonly Lazy<ICurrencyRepository> _currencyRepository;
        private readonly Lazy<IInvoiceRepository> _invoiceRepository;
        private readonly Lazy<IManufacturerRepository> _manufacturerRepository;
        private readonly Lazy<IProductMovementRepository> _productMovementRepository;
        private readonly Lazy<IProductRepository> _productRepository;
        private readonly Lazy<IStoragePlaceRepository> _storagePlaceRepository;
        private readonly Lazy<IUnitRepository> _unitRepository;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IWarehouseRepository> _warehouseRepository;
        private readonly Lazy<IPaymentRepository> _paymentRepository;
        private readonly Lazy<IInvoicePaymentsRepository> _invoicePaymentsRepository;
        private readonly Lazy<IJournalRepository> _journalRepository;
        private readonly Lazy<IRoleRepository> _rolesRepository;
        private readonly Lazy<IUserRolesRepository> _userRolesRepository;
        private readonly Lazy<IConversionRepository> _conversionRepository;

        public UnitOfWork(ApplicationDbContext dbContext, Lazy<IAccountRepository> accountRepository, Lazy<ICategoryRepository> categoryRepository, Lazy<ICurrencyAmountRepository> currencyAmountRepository, 
            Lazy<ICurrencyRepository> currencyRepository, Lazy<IInvoiceRepository> invoiceRepository, Lazy<IManufacturerRepository> manufacturerRepository, Lazy<IProductMovementRepository> productMovementRepository, 
            Lazy<IProductRepository> productRepository, Lazy<IStoragePlaceRepository> storagePlaceRepository, Lazy<IUnitRepository> unitRepository, Lazy<IUserRepository> userRepository, 
            Lazy<IWarehouseRepository> warehouseRepository, Lazy<IPaymentRepository> paymentRepository, Lazy<IInvoicePaymentsRepository> invoicePaymentsRepository, Lazy<IJournalRepository> journalRepository,
            Lazy<IRoleRepository> rolesRepository, Lazy<IUserRolesRepository> userRolesRepository, Lazy<IConversionRepository> conversionRepository)
        {
            _dbContext = dbContext;
            _transaction =  _dbContext.Database.BeginTransaction();
            _accountRepository = accountRepository;
            _categoryRepository = categoryRepository;
            _currencyAmountRepository = currencyAmountRepository;
            _currencyRepository = currencyRepository;
            _invoiceRepository = invoiceRepository;
            _manufacturerRepository = manufacturerRepository;
            _productMovementRepository = productMovementRepository;
            _productRepository = productRepository;
            _storagePlaceRepository = storagePlaceRepository;
            _unitRepository = unitRepository;
            _userRepository = userRepository;
            _warehouseRepository = warehouseRepository;
            _paymentRepository = paymentRepository;
            _invoicePaymentsRepository = invoicePaymentsRepository;
            _journalRepository = journalRepository;
            _rolesRepository = rolesRepository;
            _userRolesRepository = userRolesRepository;
            _conversionRepository = conversionRepository;
        }
        public IAccountRepository AccountRepository => _accountRepository.Value;
        public ICategoryRepository CategoryRepository => _categoryRepository.Value;
        public ICurrencyAmountRepository CurrencyAmountRepository => _currencyAmountRepository.Value;
        public ICurrencyRepository CurrencyRepository => _currencyRepository.Value;
        public IInvoiceRepository InvoiceRepository => _invoiceRepository.Value;
        public IManufacturerRepository ManufacturerRepository => _manufacturerRepository.Value;
        public IProductMovementRepository ProductMovementRepository => _productMovementRepository.Value;
        public IProductRepository ProductRepository => _productRepository.Value;
        public IStoragePlaceRepository StoragePlaceRepository => _storagePlaceRepository.Value;
        public IUnitRepository UnitRepository => _unitRepository.Value;
        public IUserRepository UserRepository => _userRepository.Value;
        public IWarehouseRepository WarehouseRepository => _warehouseRepository.Value;
        public IPaymentRepository PaymentRepository => _paymentRepository.Value;
        public IInvoicePaymentsRepository InvoicePaymentsRepository => _invoicePaymentsRepository.Value;
        public IJournalRepository JournalRepository => _journalRepository.Value;
        public IRoleRepository RoleRepository => _rolesRepository.Value;
        public IUserRolesRepository UserRolesRepository => _userRolesRepository.Value;
        public IConversionRepository ConversionRepository => _conversionRepository.Value;

        public void Commit()
        {
            _transaction.Commit();
        }

        public Task CommitAsync()
        {
            return _transaction.CommitAsync();
        }

        public void Dispose()
        {
            _transaction.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
