namespace StoreHouse360.Domain.Entities
{
    public interface IEntity
    {

    };

    public class BaseEntity<TKey> : IEntity
    {
        public virtual TKey Id { get; set; }
    }

}
