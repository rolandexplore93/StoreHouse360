using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Products;

namespace StoreHouse360.DTO.Notifications
{
    public class NotificationVM : IMapFrom<Notification>, IViewModel
    {
        public int Id { get; set; }
        public ProductVM Product { get; set; }
        public NotificationType NotificationType { get; set; }
        public bool IsValid { get; set; }
    }
}
