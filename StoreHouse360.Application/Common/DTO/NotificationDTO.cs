using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Common.DTO
{
    public class NotificationDTO
    {
        public int ObjectId { get; }
        public NotificationType NotificationType { get; }

        public NotificationDTO(int objectId, NotificationType notificationType)
        {
            ObjectId = objectId;
            NotificationType = notificationType;
        }
    }
}