using MediatR;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Categories
{
    [Authorize(Method = Method.Write, Resource = Resource.Categories)]
    public class CreateCategoryCommand : IRequest<string>
    {
        public string Name { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, string>
    {
        private readonly ICategoryRepository _categoryRepository;
        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<string> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name
            };

            var saveAction = await _categoryRepository.CreateAsync(category);
            var createdCategory = await saveAction();
            return createdCategory != null ? $"{createdCategory.Name} category created successfully." : "operation failed...";
            //return createdCategory.Id;
        }
    }
}
