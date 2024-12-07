using AutoMapper;
using StoreHouse360.Domain.Entities;
using StoreHouse360.Dto.Users;

namespace StoreHouse360.Dto.Common
{
    public static class ViewModelExtensions
    {
        public static UserVM ToViewModel(this User user, IMapper mapper)
        {
            var vm = mapper.Map<UserVM>(user);
            return vm;
        }
        public static IEnumerable<UserVM> ToViewModels(this IEnumerable<User> users, IMapper mapper)
        {
            return users.Select(user => user.ToViewModel(mapper));
        }
    }
}
