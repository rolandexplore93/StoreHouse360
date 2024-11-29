using AutoMapper;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Common.Mappings
{
    public interface IMapFrom<TkFrom>
    {
        void Map(Profile profile)
        {
            profile.CreateMap(typeof(TkFrom), GetType());
            profile.CreateMap(GetType(), typeof(TkFrom));
        }
    }
}
