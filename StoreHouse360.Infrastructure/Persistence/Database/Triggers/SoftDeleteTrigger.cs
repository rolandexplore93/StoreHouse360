using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;
using StoreHouse360.Infrastructure.Persistence.Database.Models.Common;

namespace StoreHouse360.Infrastructure.Persistence.Database.Triggers
{
    public class SoftDeleteTrigger : IBeforeSaveTrigger<ISoftDelete>
    {
        private readonly ApplicationDbContext _dbContext;
        public SoftDeleteTrigger(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task BeforeSave(ITriggerContext<ISoftDelete> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Deleted)
            {
                var entry = _dbContext.Entry(context.Entity);
                context.Entity.DeletedAt = DateTime.Now;
                context.Entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
            await Task.CompletedTask;
        }
    }
}
