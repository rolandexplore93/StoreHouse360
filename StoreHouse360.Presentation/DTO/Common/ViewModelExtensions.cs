using AutoMapper;
using StoreHouse360.Application.Common.Models;
using StoreHouse360.Domain.Entities;
using StoreHouse360.DTO.Pagination;

namespace StoreHouse360.DTO.Common
{
    public static class ViewModelExtensions
    {
        public static TViewModel ToViewModel<TViewModel>(this IEntity entity, IMapper mapper) where TViewModel : IViewModel
        {
            var vm = mapper.Map<TViewModel>(entity);
            return vm;
        }
        public static IEnumerable<TViewModel> ToViewModels<TViewModel>(this IEnumerable<IEntity> entities, IMapper mapper) where TViewModel : IViewModel
        {
            //return entities.Select(entity => entity.ToViewModel<TViewModel>(mapper));
            return mapper.Map<IEnumerable<IEntity>, IEnumerable<TViewModel>>(entities);
        }

        public static PaginationVM<TViewModel> ToViewModel<TViewModel>(this IPaginatedCollections<IEntity> page, IMapper mapper) where TViewModel : IViewModel
        {
            return new PaginationVM<TViewModel>
            {
                Data = page.ToViewModels<TViewModel>(mapper),
                CurrentPage = page.CurrentPage,
                PagesCount = page.PagesCount,
                PageSize = page.PageSize,
                RowsCount = page.RowsCount
            };
        }
    }
}
