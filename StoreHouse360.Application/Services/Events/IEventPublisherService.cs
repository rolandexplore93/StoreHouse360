using StoreHouse360.Domain.Events;

namespace StoreHouse360.Application.Services.Events
{
    public interface IEventPublisherService
    {
        public Task Publish(DomainEvent @event);
        public Task PublishAsync(DomainEvent @event)
        {
            Publish(@event);
            return Task.CompletedTask;
        }
    }
}
