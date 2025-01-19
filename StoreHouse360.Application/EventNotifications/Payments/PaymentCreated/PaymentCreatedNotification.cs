using MediatR;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.EventNotifications.Payments.PaymentCreated
{
    public class PaymentCreatedNotification : INotification
    {
        public readonly Payment Payment;
        public PaymentCreatedNotification(Payment payment)
        {
            Payment = payment;
        }
    }
}