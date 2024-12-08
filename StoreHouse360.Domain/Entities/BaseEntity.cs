namespace StoreHouse360.Domain.Entities
{
    public class BaseEntity<TKey> : IEntity
    {
        public virtual TKey Id { get; set; }
    }

    public interface IEntity { };
}
