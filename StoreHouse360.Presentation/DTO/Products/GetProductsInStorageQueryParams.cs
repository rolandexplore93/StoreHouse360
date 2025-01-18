using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Application.Queries.Products;
using StoreHouse360.DTO.Pagination;

namespace StoreHouse360.DTO.Products
{
    public class GetProductsInStorageQueryParams : PaginationRequestParams, IMapFrom<GetAllProductsInStoragePlaceQuery>
    {
        public int StoragePlaceId { get; set; }
        public bool? IncludeStoragePlaceChildren { get; set; } = true;
    }
}
