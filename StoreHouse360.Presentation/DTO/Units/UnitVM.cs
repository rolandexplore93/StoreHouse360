using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Dto.Common;

namespace StoreHouse360.Dto.Units
{
    public class UnitVM : IMapFrom<Domain.Entities.Unit>, IViewModel
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int Value { get; init; }
    }
}
