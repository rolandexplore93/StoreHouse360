using MediatR;
using StoreHouse360.Domain.Events;

namespace StoreHouse360.Application.Common.Events
{
    public class DomainNotification<T> : INotification where T : DomainEvent
    {
        public T DomainEvent { get; set; }
        public DomainNotification(T domainEvent)
        {
            DomainEvent = domainEvent;
        }
    }
}