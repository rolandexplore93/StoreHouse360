using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using StoreHouse360.DTO.Common;

namespace StoreHouse360.DTO.Notifications
{
    public class NotificationVM : IMapFrom<Notification>, IViewModel
    {
        public int Id { get; set; }
        public int ObjectId { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}
