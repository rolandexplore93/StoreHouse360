using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Categories
{
    public class GetCategoryQuery : IRequest<Category>
    {
        public int Id { get; init; }
    }

    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, Category>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoryQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public Task<Category> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            return _categoryRepository.FindByIdAsync(request.Id);
        }
    }
}
