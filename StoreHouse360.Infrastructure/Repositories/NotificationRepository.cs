using AutoMapper;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;
using StoreHouse360.Infrastructure.Persistence.Database.Models;
using StoreHouse360.Infrastructure.Persistence.Database;

namespace StoreHouse360.Infrastructure.Repositories
{
    public class NotificationRepository : RepositoryCrud<Notification, NotificationDb>, INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
