﻿using StoreHouse360.Application.Common.QueryFilters;

namespace StoreHouse360.Application.Common.DTO
{
    [QueryFiltersConcat(ConcatType = QueryFilterConcatType.And)]
    public class ProductMovementFiltersDTO
    {
        [QueryFilter(QueryFilterCompareType.InArray, FieldName = "ProductId")]
        public IList<int> ProductIds { get; set; }

        [QueryFilter(QueryFilterCompareType.Equal)]
        public int? CategoryId { get; set; }

        [QueryFilter(QueryFilterCompareType.Equal)]
        public int? AccountId { get; set; }

        [QueryFilter(QueryFilterCompareType.GreaterThanOrEqual, FieldName = "CreatedAt")]
        public DateTime? StartDate { get; set; }

        [QueryFilter(QueryFilterCompareType.LessThanOrEqual, FieldName = "CreatedAt")]
        public DateTime? EndDate { get; set; }

        [QueryFilter(QueryFilterCompareType.Equal)]
        public int? WarehouseId { get; set; }

        [QueryFilter(QueryFilterCompareType.Equal)]
        public int? ManufacturerId { get; set; }

        [QueryFilter(QueryFilterCompareType.Equal)]
        public int? StoragePlaceId { get; set; }
    }
}
