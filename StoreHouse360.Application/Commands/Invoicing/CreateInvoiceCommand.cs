﻿using MediatR;
using StoreHouse360.Application.Commands.Invoicing.DTO;
using StoreHouse360.Application.Queries.Invoicing;
using StoreHouse360.Application.Queries.Invoicing.DTO;
using StoreHouse360.Application.Repositories.UnitOfWork;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Invoicing
{
    public class CreateInvoiceCommand : IRequest<int>
    {
        public int AccountId { get; set; }
        public int WarehouseId { get; set; }
        public int CurrencyId { get; set; }
        public string? Note { get; set; }
        public InvoiceType Type { get; set; }
        public IEnumerable<InvoiceItemDTO> Items { get; set; }
    }

    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, int>
    {
        private readonly Lazy<IUnitOfWork> _unitOfWork;
        private readonly IMediator _mediator;

        public CreateInvoiceCommandHandler(Lazy<IUnitOfWork> unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        public async Task<int> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            if (request.Type == InvoiceType.Out)
            {
                var checkProductQuantityQuery = new CheckProductQuantityQuery
                {
                    ProductQuantities = request.Items.Select( item => new CheckProductQuantityDTO { ProductId = item.ProductId, Quantity = item.Quantity })
                };

                await _mediator.Send(checkProductQuantityQuery, cancellationToken);
            }

            using var unitOfWork = _unitOfWork.Value;
                Invoice invoice = new Invoice
                {
                    AccountId = request.AccountId,
                    WarehouseId = request.WarehouseId,
                    CurrencyId = request.CurrencyId,
                    TotalPrice = request.Items.Sum(item => item.UnitPrice * item.Quantity),
                    Note = request.Note,
                    CreatedAt = DateTime.Now,
                    Type = request.Type,
                    Status = InvoiceStatus.Opened
                };

                var saveInvoiceAction = await unitOfWork.InvoiceRepository.CreateAsync(invoice);
                var savedInvoiceEntity = await saveInvoiceAction.Invoke();

                var productMovements = request.Items.Select(
                    dto => new ProductMovement
                    {
                        InvoiceId = savedInvoiceEntity.Id,
                        ProductId = dto.ProductId,
                        PlaceId = dto.PlaceId,
                        Quantity = dto.Quantity,
                        UnitPrice = dto.UnitPrice,
                        TotalPrice = dto.UnitPrice * dto.Quantity,
                        CurrencyId = dto.CurrencyId,
                        Note = dto.Note,
                        Type = ProductMovement.TypeFromInvoice(request.Type),
                        CreatedAt = DateTime.Now
                    }
                );

                var saveMovementsAction = await unitOfWork.ProductMovementRepository.CreateAllAsync(productMovements);
                var savedProductMovementEntities = await saveMovementsAction.Invoke();

                var currencyAmounts = new List<CurrencyAmount>();

                savedProductMovementEntities.ToList()
                    .Zip(request.Items)
                    .ToList()
                    .ForEach(entry =>
                    {
                        var (savedProductMovementEntity, requestInvoiceItem) = entry;
                        var currencyAmountsForEachInvoiceItem = requestInvoiceItem.CurrencyAmounts
                            .Select(
                                dto => new CurrencyAmount
                                {
                                    ObjectId = savedProductMovementEntity.Id,
                                    Key = CurrencyAmountKey.Movement,
                                    Amount = dto.Value,
                                    CurrencyId = dto.CurrencyId
                                }
                            );
                        currencyAmounts.AddRange(currencyAmountsForEachInvoiceItem);
                    });
                var saveCurrencyAmountsAction = await unitOfWork.CurrencyAmountRepository.CreateAllAsync(currencyAmounts);
                var savedCurrencyAmountEntities = await saveCurrencyAmountsAction.Invoke();
                return savedInvoiceEntity.Id;
            }

        
    }

}
