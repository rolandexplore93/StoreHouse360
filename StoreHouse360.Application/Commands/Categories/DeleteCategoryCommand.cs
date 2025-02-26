using MediatR;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories;

namespace StoreHouse360.Application.Commands.Categories
{
    [Authorize(Method = Method.Delete, Resource = Resource.Categories)]
    public class DeleteCategoryCommand : IRequest
    {
        public int Id { get; init; }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            await _categoryRepository.DeleteAsync(request.Id);
            await _categoryRepository.SaveChanges();
        }
    }
}
