using MediatR;
using StoreHouse360.Application.Common.DTO;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Notifications
{
    public class CreateNotificationsCommand : IRequest<IEnumerable<int>>
    {
        public IEnumerable<NotificationDTO> NotificationDTOs { get; }
        public CreateNotificationsCommand(IEnumerable<NotificationDTO> notificationDTOs)
        {
            NotificationDTOs = notificationDTOs;
        }
    }

    public class CreateNotificationsCommandHandler : IRequestHandler<CreateNotificationsCommand, IEnumerable<int>>
    {
        private readonly INotificationRepository _notificationRepository;
        public CreateNotificationsCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public async Task<IEnumerable<int>> Handle(CreateNotificationsCommand request, CancellationToken cancellationToken)
        {
            var saveNotificationsAction = await _notificationRepository.CreateAllAsync(
                request.NotificationDTOs.Select(dto => new Notification
                    {
                        ObjectId = dto.ObjectId,
                        NotificationType = dto.NotificationType
                    }
                )
            );
            var notifications = await saveNotificationsAction();
            return notifications.Select(notification => notification.Id);
        }
    }
}
