using MediatR;
using StoreHouse360.Application.Queries.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Categories
{
    public class GetAllCategoriesQuery : GetPaginatedQuery<Category>
    {
    }
    public class GetAllCategoriesQueryHandler : PaginatedQueryHandler<GetAllCategoriesQuery, Category>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesQueryHandler(ICategoryRepository repository)
        {
            _categoryRepository = repository;
        }

        protected override async Task<IQueryable<Category>> GetQuery(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories;
        }
    }
}
