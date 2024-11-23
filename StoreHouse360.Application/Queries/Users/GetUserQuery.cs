using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Users
{
    public class GetUserQuery : IRequest<User>
    {
        public int Id { get; set; }
    }
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
    {
        private readonly IUserRepository _userRepository;
        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.FindByIdAsync(request.Id);
        }
    }
}
