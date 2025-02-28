using MediatR;
using StoreHouse360.Application.Commands.Journals;
using StoreHouse360.Application.Queries.Invoicing;
using StoreHouse360.Application.Settings;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.EventNotifications.Payments.PaymentCreated
{
    public class PaymentJournalHandler : INotificationHandler<PaymentCreatedNotification>
    {
        private readonly IMediator _mediator;
        private readonly AppSettings _appSettings;
        public PaymentJournalHandler(IMediator mediator, AppSettings appSettings)
        {
            _mediator = mediator;
            _appSettings = appSettings;
        }

        public async Task Handle(PaymentCreatedNotification notification, CancellationToken cancellationToken)
        {
            var payment = notification.Payment;

            var invoice = await _mediator.Send(new GetInvoiceQuery { Id = payment.InvoiceId }, cancellationToken);
            if (payment.PaymentIoType == PaymentIoType.In)
            {
                await _handleFromCustomerToCashDrawer(invoice, payment, cancellationToken);
                await _handleFromCashDrawerToSales(payment, cancellationToken);
            }
            else if (payment.PaymentIoType == PaymentIoType.Out)
            {
                await _handleFromCashDrawerToCustomer(invoice, payment, cancellationToken);
                await _handleFromPurchasesToCashDrawer(payment, cancellationToken);
            }
        }
        private async Task _handleFromCustomerToCashDrawer(Invoice invoice, Payment payment, CancellationToken cancellationToken)
        {
            int defaultCashDrawerAccountId = _appSettings.DefaultMainCashDrawerAccountId;
            var createJournalsCommand =
                new CreateJournalsCommand(
                    invoice.AccountId.GetValueOrDefault(),
                    defaultCashDrawerAccountId,
                    payment.Amount,
                    payment.CurrencyId
                );
            await _mediator.Send(createJournalsCommand, cancellationToken);
        }
        private async Task _handleFromCashDrawerToSales(Payment payment, CancellationToken cancellationToken)
        {
            int defaultCashDrawerAccountId = _appSettings.DefaultMainCashDrawerAccountId;
            int defaultSalesAccountId = _appSettings.DefaultSalesAccountId;
            var createJournalsCommand =
                new CreateJournalsCommand(
                    defaultCashDrawerAccountId,
                    defaultSalesAccountId,
                    payment.Amount,
                    payment.CurrencyId
                );
            await _mediator.Send(createJournalsCommand, cancellationToken);
        }
        private async Task _handleFromCashDrawerToCustomer(Invoice invoice, Payment payment, CancellationToken cancellationToken)
        {
            int defaultCashDrawerAccountId = _appSettings.DefaultMainCashDrawerAccountId;
            var createJournalsCommand =
                new CreateJournalsCommand(
                    defaultCashDrawerAccountId,
                    invoice.AccountId.GetValueOrDefault(),
                    payment.Amount,
                    payment.CurrencyId
                );
            await _mediator.Send(createJournalsCommand, cancellationToken);
        }
        private async Task _handleFromPurchasesToCashDrawer(Payment payment, CancellationToken cancellationToken)
        {
            int defaultPurchasesAccountId = _appSettings.DefaultPurchasesAccountId;
            int defaultCashDrawerAccountId = _appSettings.DefaultMainCashDrawerAccountId;
            var createJournalsCommand =
                new CreateJournalsCommand(
                    defaultPurchasesAccountId,
                    defaultCashDrawerAccountId,
                    payment.Amount,
                    payment.CurrencyId
                );
            await _mediator.Send(createJournalsCommand, cancellationToken);
        }
    }
}
