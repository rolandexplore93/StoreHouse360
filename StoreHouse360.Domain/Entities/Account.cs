namespace StoreHouse360.Domain.Entities
{
    public class Account : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
    }
}
