using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Notifications
{
    public class GetAllNotificationsQuery : GetPaginatedQuery<Notification>
    {
        public IEnumerable<int> ObjectIds { get; set; } = new List<int>();
        public NotificationType? NotificationType { get; set; } = default;
        public bool IsValid { get; set; } = false;
    }

    public class GetAllNotificationsQueryHandler : PaginatedQueryHandler<GetAllNotificationsQuery, Notification>
    {
        private readonly INotificationRepository _notificationRepository;
        public GetAllNotificationsQueryHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        protected override async Task<IQueryable<Notification>> GetQuery(GetAllNotificationsQuery request, CancellationToken cancellationToken)
        {
            var notifications = await _notificationRepository.GetAllAsync();
            var query = notifications
                .Where(notification => request.ObjectIds.Any(objId => notification.ObjectId == objId) || !request.ObjectIds.Any())
                .Where(notification => notification.NotificationType == request.NotificationType || request.NotificationType == default)
                .Where(notification => notification.IsValid == request.IsValid || !request.IsValid);

            return query;
        }
    }
}
