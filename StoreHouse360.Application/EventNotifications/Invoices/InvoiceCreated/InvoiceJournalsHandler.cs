using MediatR;
using StoreHouse360.Application.Commands.Journals;
using StoreHouse360.Application.Settings;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.EventNotifications.Invoices.InvoiceCreated
{
    public class InvoiceJournalsHandler : INotificationHandler<InvoiceCreatedNotification>
    {
        private readonly IMediator _mediator;
        private readonly AppSettings _appSettings;
        public InvoiceJournalsHandler(IMediator mediator, AppSettings appSettings)
        {
            _mediator = mediator;
            _appSettings = appSettings;
        }

        public async Task Handle(InvoiceCreatedNotification notification, CancellationToken cancellationToken)
        {
            var invoice = notification.Invoice;
            if (invoice.Type == InvoiceType.Out)
            {
                //await _handleFromCashDrawerToCustomer(invoice, cancellationToken);
                //await _handleFromSalesToCashDrawer(invoice, cancellationToken);

                if (invoice.AccountType.DealsWithPurchasesSales())
                {
                    await _handleFromCashDrawerToCustomer(invoice, cancellationToken);
                    await _handleFromSalesToCashDrawer(invoice, cancellationToken);
                }

                if (invoice.AccountType.DealsWithReturns())
                {
                    await _handleFromSalesReturnsToCustomer(invoice, cancellationToken);
                }
            }
            else if (invoice.Type == InvoiceType.In)
            {
                //await _handleFromCustomerToCashDrawer(invoice, cancellationToken);
                //await _handleFromCashDrawerToPurchases(invoice, cancellationToken);

                if (invoice.AccountType.DealsWithPurchasesSales())
                {
                    await _handleFromCustomerToCashDrawer(invoice, cancellationToken);
                    await _handleFromCashDrawerToPurchases(invoice, cancellationToken);
                }

                if (invoice.AccountType.DealsWithReturns())
                {
                    await _handleFromCustomerToPurchasesReturns(invoice, cancellationToken);
                }
            }
        }

        private async Task _handleFromCashDrawerToCustomer(Invoice invoice, CancellationToken cancellationToken)
        {
            int defaultCashDrawerAccountId = _appSettings.DefaultMainCashDrawerAccountId;
            int defaultCurrencyId = _appSettings.DefaultCurrencyId;
            var createJournalsCommand =
                new CreateJournalsCommand(
                    defaultCashDrawerAccountId,
                    invoice.AccountId.GetValueOrDefault(),
                    invoice.TotalPrice,
                    invoice.CurrencyId.GetValueOrDefault(defaultCurrencyId)
                );

            await _mediator.Send(createJournalsCommand, cancellationToken);
        }
        private async Task _handleFromSalesToCashDrawer(Invoice invoice, CancellationToken cancellationToken)
        {
            int defaultCashDrawerAccountId = _appSettings.DefaultMainCashDrawerAccountId;
            int defaultSalesAccountId = _appSettings.DefaultSalesAccountId;
            int defaultCurrencyId = _appSettings.DefaultCurrencyId;
            var createJournalsCommand =
                new CreateJournalsCommand(
                    defaultSalesAccountId,
                    defaultCashDrawerAccountId,
                    invoice.TotalPrice,
                    invoice.CurrencyId.GetValueOrDefault(defaultCurrencyId)
                );

            await _mediator.Send(createJournalsCommand, cancellationToken);
        }
        private async Task _handleFromCustomerToCashDrawer(Invoice invoice, CancellationToken cancellationToken)
        {
            int defaultCashDrawerAccountId = _appSettings.DefaultMainCashDrawerAccountId;
            int defaultCurrencyId = _appSettings.DefaultCurrencyId;
            var createJournalsCommand =
                new CreateJournalsCommand(
                    invoice.AccountId.GetValueOrDefault(),
                    defaultCashDrawerAccountId,
                    invoice.TotalPrice,
                    invoice.CurrencyId.GetValueOrDefault(defaultCurrencyId)
                );

            await _mediator.Send(createJournalsCommand, cancellationToken);
        }
        private async Task _handleFromCashDrawerToPurchases(Invoice invoice, CancellationToken cancellationToken)
        {
            int defaultPurchasesAccountId = _appSettings.DefaultPurchasesAccountId;
            int defaultCashDrawerAccountId = _appSettings.DefaultMainCashDrawerAccountId;
            int defaultCurrencyId = _appSettings.DefaultCurrencyId;
            var createJournalsCommand =
                new CreateJournalsCommand(
                    defaultCashDrawerAccountId,
                    defaultPurchasesAccountId,
                    invoice.TotalPrice,
                    invoice.CurrencyId.GetValueOrDefault(defaultCurrencyId)
                );

            await _mediator.Send(createJournalsCommand, cancellationToken);
        }

        private async Task _handleFromSalesReturnsToCustomer(Invoice invoice, CancellationToken cancellationToken)
        {
            var createJournalsCommand =
                new CreateJournalsCommand(
                    _appSettings.DefaultSalesReturnsAccountId,
                    invoice.AccountId.GetValueOrDefault(),
                    invoice.TotalPrice,
                    invoice.CurrencyId.GetValueOrDefault(_appSettings.DefaultCurrencyId)
                );

            await _mediator.Send(createJournalsCommand, cancellationToken);
        }

        private async Task _handleFromCustomerToPurchasesReturns(Invoice invoice, CancellationToken cancellationToken)
        {
            var createJournalsCommand =
                new CreateJournalsCommand(
                    invoice.AccountId.GetValueOrDefault(),
                    _appSettings.DefaultPurchasesReturnsAccountId,
                    invoice.TotalPrice,
                    invoice.CurrencyId.GetValueOrDefault(_appSettings.DefaultCurrencyId)
                );

            await _mediator.Send(createJournalsCommand, cancellationToken);
        }
    }
}
