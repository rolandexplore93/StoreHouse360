using StoreHouse360.Application.Common.Mappings;

namespace StoreHouse360.Infrastructure.Models
{
    public class Category : IMapFrom<Domain.Entities.Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
