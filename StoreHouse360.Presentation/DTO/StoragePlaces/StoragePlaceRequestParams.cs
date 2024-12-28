using Microsoft.AspNetCore.Mvc;
using StoreHouse360.DTO.Pagination;

namespace StoreHouse360.DTO.StoragePlaces
{
    public class StoragePlaceRequestParams : PaginationRequestParams
    {
        [FromQuery(Name = "is_parent")]
        public bool? IsParent { get; set; }
        [FromQuery(Name = "container_id")]
        public int? ContainerId { get; set; }
    }
}
