﻿using MediatR;
using StoreHouse360.Application.Commands.Invoicing.DTO;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.EventNotifications.Invoices.InvoiceCreated;
using StoreHouse360.Application.Queries.Invoicing;
using StoreHouse360.Application.Queries.Invoicing.DTO;
using StoreHouse360.Application.Repositories.UnitOfWork;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Invoicing
{
    [Authorize(Method = Method.Write, Resource = Resource.Invoices)]
    public class CreateInvoiceCommand : IRequest<int>
    {
        public int AccountId { get; set; }
        public int WarehouseId { get; set; }
        public int CurrencyId { get; set; }
        public string? Note { get; set; }
        public InvoiceType Type { get; set; }
        public InvoiceAccountType AccountType { get; set; }
        public IEnumerable<InvoiceItemDTO> Items { get; set; }
        public bool IgnoreMinLevelWarnings { get; set; }
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
                    ProductQuantities = request.Items.Select(item => new CheckProductQuantityDTO { ProductId = item.ProductId, Quantity = item.Quantity }),
                    IgnoreMinLevelWarnings = request.IgnoreMinLevelWarnings

                };

                await _mediator.Send(checkProductQuantityQuery, cancellationToken);
            }


            var invoice = new Invoice(
                accountId: request.AccountId,
                warehouseId: request.WarehouseId,
                currencyId: request.CurrencyId,
                note: request.Note,
                createdAt: DateTime.Now,
                type: request.Type,
                accountType: request.AccountType,
                items: request.Items.Select(dto => _buildItem(dto, request.Type)).ToList()
            );

            request.Items
                .Select(dto => _buildItem(dto, request.Type))
                .ToList()
                .ForEach(movement => invoice.AddItem(movement));

            using (var unitOfWork = _unitOfWork.Value)
            {
                var saveInvoiceAction = await unitOfWork.InvoiceRepository.CreateAsync(invoice);
                invoice = await saveInvoiceAction.Invoke();

                await _mediator.Publish(new InvoiceCreatedNotification(invoice), cancellationToken);

                await unitOfWork.CommitAsync();
            };

            return invoice.Id;
        }

        private static ProductMovement _buildItem(InvoiceItemDTO dto, InvoiceType invoiceType)
        {
            return new ProductMovement
            {
                ProductId = dto.ProductId,
                PlaceId = dto.PlaceId,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice,
                CurrencyId = dto.CurrencyId,
                Note = dto.Note,
                Type = ProductMovement.TypeFromInvoice(invoiceType),
                CreatedAt = DateTime.Now,
                CurrencyAmounts = dto.CurrencyAmounts?.Select(currencyAmountDto => new CurrencyAmount
                {
                    Amount = currencyAmountDto.Value,
                    CurrencyId = currencyAmountDto.CurrencyId,
                    Key = CurrencyAmountKey.Movement
                })
            };


        }

    }
}
