namespace StoreHouse360.Domain.Entities
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
