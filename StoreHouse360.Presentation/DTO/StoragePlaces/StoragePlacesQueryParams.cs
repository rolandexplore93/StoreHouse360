using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Application.Queries.StoragePlaces;
using StoreHouse360.DTO.Pagination;

namespace StoreHouse360.DTO.StoragePlaces
{
    public class StoragePlacesQueryParams : PaginationRequestParams, IMapFrom<GetAllStoragePlacesQuery>
    {
        [FromQuery(Name = "name")]
        public string? Name { get; set; }
        [FromQuery(Name = "is_parent")]
        public bool? IsParent { get; set; }
        [FromQuery(Name = "container_id")]
        public int? ContainerId { get; set; }
    }
}
