namespace StoreHouse360.Infrastructure.Persistence.Database.Models.Common
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
