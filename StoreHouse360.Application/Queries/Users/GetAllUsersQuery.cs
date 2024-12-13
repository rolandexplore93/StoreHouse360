using MediatR;
using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Users
{
    public class GetAllUsersQuery : GetPaginatedQuery<User>
    {
    }

    public class GetAllUsersQueryHandler : PaginatedQueryHandler<GetAllUsersQuery, User>
    {

        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected override async Task<IQueryable<User>> GetQuery(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();
            return users;
        }
    }
}
