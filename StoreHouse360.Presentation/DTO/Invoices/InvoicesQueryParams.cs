using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Application.Queries.Invoicing;
using StoreHouse360.Domain.Entities;
using StoreHouse360.DTO.Pagination;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.DTO.Invoices
{
    public class InvoicesQueryParams : PaginationRequestParams, IMapFrom<GetAllInvoicesQuery>
    {
        [FromQuery(Name = "type")]
        public InvoiceType? Type { get; set; } = default;

        [Range(0, int.MaxValue)]
        [FromQuery(Name = "account_id")]
        public int? AccountId { get; set; } = default;

        [Range(0, int.MaxValue)]
        [FromQuery(Name = "warehouse_id")]
        public int? WarehouseId { get; set; } = default;
    }
}
