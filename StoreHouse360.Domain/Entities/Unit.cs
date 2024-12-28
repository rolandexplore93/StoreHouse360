namespace StoreHouse360.Domain.Entities
{
    public class Unit : BaseEntity<int>
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
