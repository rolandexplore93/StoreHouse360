﻿using MediatR;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.EventNotifications.Invoices.InvoiceCreated
{
    public class InvoiceCreatedNotification : INotification
    {
        public readonly Invoice Invoice;
        public InvoiceCreatedNotification(Invoice invoice)
        {
            Invoice = invoice;
        }
    }
}
