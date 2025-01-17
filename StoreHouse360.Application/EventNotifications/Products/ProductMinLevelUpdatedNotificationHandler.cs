using MediatR;
using StoreHouse360.Application.Commands.Notifications;
using StoreHouse360.Application.Common.DTO;
using StoreHouse360.Application.Common.Events;
using StoreHouse360.Application.Queries.Notifications;
using StoreHouse360.Application.Queries.Warehouses;
using StoreHouse360.Domain.Aggregations;
using StoreHouse360.Domain.Entities;
using StoreHouse360.Domain.Events;
using System.Diagnostics;

namespace StoreHouse360.Application.EventNotifications.Products
{
    public class ProductMinLevelUpdatedNotificationHandler : INotificationHandler<DomainNotification<ProductMinimumLevelUpdated>>
    {
        private readonly IMediator _mediator;
        public ProductMinLevelUpdatedNotificationHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(DomainNotification<ProductMinimumLevelUpdated> notification, CancellationToken cancellationToken)
        {
            var @event = notification.DomainEvent;
            try
            {
                if (@event.MinimumLevelIncreased())
                    await _handleIncreased(@event);
                if (@event.MinimumLevelDecreased())
                    await _handleDecreased(@event);
            }
            catch (Exception e)
            {
                Debug.Fail(e.ToString());
            }
        }

        private async Task _handleIncreased(ProductMinimumLevelUpdated @event)
        {
            AggregateProductQuantity aggregate = await _aggregateTask(@event.ProductId);
            // Quantity is still below the updated min level
            if (aggregate.QuantitySum > @event.MinimumLevelAfter)
            {
                // No need to do anything
            }
            // Quantity is now below the updated min level
            else
            {
                var notificationsWithProductId = await _getNotificationsWithProductId(@event.ProductId);
                if (notificationsWithProductId.Any())
                {
                    return;
                }
                var createdNotificationDtos = new List<NotificationDTO>
            {
                new(@event.ProductId, NotificationType.MinLevelExceeded)
            };
                await _mediator.Send(new CreateNotificationsCommand(createdNotificationDtos));
            }
        }

        private async Task _handleDecreased(ProductMinimumLevelUpdated @event)
        {
            AggregateProductQuantity aggregate = await _aggregateTask(@event.ProductId);
            // Quantity is still above the updated min level
            if (aggregate.QuantitySum < @event.MinimumLevelAfter)
            {
                // No need to do anything
            }
            // Quantity is now above the updated min level
            else
            {
                var notificationsWithProductId = await _getNotificationsWithProductId(@event.ProductId);
                notificationsWithProductId.ForEach(notification => notification.IsValid = false);
                await _mediator.Send(new UpdateNotificationsCommand(notificationsWithProductId));
            }
        }

        private async Task<AggregateProductQuantity> _aggregateTask(int productId)
        {
            var inventoryQueryOfProduct = new InventoryWarehouseQuery
            {
                Filters = new ProductMovementFiltersDTO { ProductIds = new List<int> { productId } }
            };
            var aggregatesPage = await _mediator.Send(inventoryQueryOfProduct);
            var aggregate = aggregatesPage.ToList().First();
            return aggregate;
        }
        private async Task<List<Notification>> _getNotificationsWithProductId(int productId)
        {
            var getAllNotificationQuery = new GetAllNotificationsQuery
            {
                ObjectIds = new List<int> { productId },
                IsValid = true,
                NotificationType = NotificationType.MinLevelExceeded,
                Page = 1,
                PageSize = int.MaxValue
            };
            var notificationsPage = await _mediator.Send(getAllNotificationQuery);
            return notificationsPage.ToList();
        }
    }
}
