using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Dto.Common;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.Dto.Category
{
    public class CategoryVM : IMapFrom<Domain.Entities.Category>, IViewModel
    {
        public int Id { get; init; }
        
        [Required] 
        public string Name { get; init; }
    }
}
