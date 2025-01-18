using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Application.Queries.Notifications;
using StoreHouse360.Domain.Entities;
using StoreHouse360.DTO.Pagination;

namespace StoreHouse360.DTO.Notifications
{
    public class GetNotificationsQueryParams : PaginationRequestParams, IMapFrom<GetAllNotificationsQuery>
    {
        public IEnumerable<int> ObjectIds { get; set; } = new List<int>();
        public NotificationType? NotificationType { get; set; } = default;
        public bool? IsValid { get; set; }
    }
}
