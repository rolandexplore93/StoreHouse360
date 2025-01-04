namespace StoreHouse360.Application.Common.DTO
{
    public class ProductMovementFiltersDTO
    {
        public IList<int>? ProductIds { get; set; }
        public int? CategoryId { get; set; }
        public int? AccountId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? WarehouseId { get; set; }
        public int? ManufacturerId { get; set; }
        public int? PlaceId { get; set; }
    }
}
