using MediatR;
using StoreHouse360.Application.Commands.Notifications;
using StoreHouse360.Application.Common.DTO;
using StoreHouse360.Application.Queries.Invoicing;
using StoreHouse360.Application.Queries.Notifications;
using StoreHouse360.Domain.Entities;
using System.Diagnostics;

namespace StoreHouse360.Application.EventNotifications.Invoices
{
    public class MinimumLevelNotificationHandler : INotificationHandler<InvoiceCreatedNotification>
    {
        private readonly IMediator _mediator;
        public MinimumLevelNotificationHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(InvoiceCreatedNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                Task handleTask = notification.Invoice.Type == InvoiceType.Out
                    ? HandleOutInvoice(notification.Invoice)
                    : HandleInInvoice(notification.Invoice);
                await handleTask;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        private async Task HandleOutInvoice(Invoice invoice)
        {
            var query = new GetProductsWithNewMinLevelWarningsQuery(invoice.Id);
            var productsWithNewMinLevelWarnings = await _mediator.Send(query);

            IList<NotificationDTO> notificationDtos = productsWithNewMinLevelWarnings
                .Select(product => new NotificationDTO(product.Id, NotificationType.MinLevelExceeded))
                .ToList();

            var command = new CreateNotificationsCommand(notificationDtos);
            var createdNotificationIds = await _mediator.Send(command);
        }
        private async Task HandleInInvoice(Invoice invoice)
        {
            var productsQuery = new GetProductsWithNewMinLevelResolvesQuery(invoice.Id);
            var productsWithNewMinLevelResolves = (await _mediator.Send(productsQuery)).ToList();
            var notificationQuery = new GetAllNotificationsQuery
            {
                Page = 1,
                PageSize = int.MaxValue,
                ObjectIds = productsWithNewMinLevelResolves.Select(product => product.Id),
                NotificationType = NotificationType.MinLevelExceeded,
                IsValid = true
            };
            var notificationsPage = await _mediator.Send(notificationQuery);
            var notifications = notificationsPage.ToList();
            var notificationsResolved = notifications
                .Where(notificationResolved => productsWithNewMinLevelResolves.Any(product => product.Id == notificationResolved.ObjectId))
                .ToList();

            notificationsResolved.ForEach(notification => notification.IsValid = false);
            var command = new UpdateNotificationsCommand(notificationsResolved);
            var updatedNotificationIds = await _mediator.Send(command);
        }
    }
}