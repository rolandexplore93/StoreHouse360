using MediatR;
using StoreHouse360.Application.Common.Models;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Queries.Common
{
    public interface IGetPaginatedQuery
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
    public class GetPaginatedQuery<TEntity> : IRequest<IPaginatedCollections<TEntity>>, IGetPaginatedQuery
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public abstract class PaginatedQueryHandler<TRequest, TEntity> : IRequestHandler<TRequest, IPaginatedCollections<TEntity>>
        where TEntity : class, IEntity
        where TRequest : GetPaginatedQuery<TEntity>
    {
        public async Task<IPaginatedCollections<TEntity>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var query = await GetQuery(request, cancellationToken);
            return query.AsPaginatedQuery(request.Page, request.PageSize);
        }

        protected abstract Task<IQueryable<TEntity>> GetQuery(TRequest request, CancellationToken cancellationToken);
    }
}
