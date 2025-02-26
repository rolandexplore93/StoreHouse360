using MediatR;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Users
{
    [Authorize(Method = Method.Update, Resource = Resource.Users)]
    public class UpdateUserCommand : IRequest
    {
        public int Id { get; set; }

        public string UserName { get; init; }
    }
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByIdAsync(request.Id);
            user.UserName = request.UserName;
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChanges();
        }
    }
}
