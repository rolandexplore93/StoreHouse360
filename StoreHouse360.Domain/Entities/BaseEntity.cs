namespace StoreHouse360.Domain.Entities
{
    public interface IEntity
    {

    };

    public class BaseEntity<TKey> : IEntity
    {
        public TKey Id { get; set; }
    }

}
