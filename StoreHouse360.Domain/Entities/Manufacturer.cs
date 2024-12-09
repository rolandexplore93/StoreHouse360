namespace StoreHouse360.Domain.Entities
{
    public class Manufacturer : BaseEntity<int>
    {
        public string Name { get; set; }
        public string? Code { get; set; }
    }
}
