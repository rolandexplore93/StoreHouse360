﻿using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Application.Queries.Payments;
using StoreHouse360.Domain.Entities;
using StoreHouse360.DTO.Pagination;

namespace StoreHouse360.DTO.Payments
{
    public class GetAllPaymentsRequestDTO : PaginationRequestParams, IMapFrom<GetAllPaymentsQuery>
    {
        [FromQuery] public int InvoiceId { get; set; } = default;
        [FromQuery] public PaymentType? PaymentType { get; set; } = default;
        [FromQuery] public PaymentIoType? PaymentIoType { get; set; } = default;
    }
}
