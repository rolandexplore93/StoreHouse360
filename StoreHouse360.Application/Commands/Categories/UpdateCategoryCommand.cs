using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Categories
{
    public class UpdateCategoryCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; init; }
    }
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, int>
    {
        private readonly ICategoryRepository _categoryRepository;
        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<int> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var updatingEntity = new Category { Id = request.Id, Name = request.Name };
            var aaa = await _categoryRepository.UpdateAsync(updatingEntity);
            await _categoryRepository.SaveChanges();
            var bbb = updatingEntity.Id;
            return updatingEntity.Id;
        }
    }
}
