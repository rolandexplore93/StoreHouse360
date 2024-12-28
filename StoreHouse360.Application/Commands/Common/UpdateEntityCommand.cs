using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Common
{
    public interface IUpdateEntityCommand<out TKey> : IRequest<TKey>
    {

    }
    public abstract class UpdateEntityCommandHandler<TRequest, TEntity, TKey, TRepository> : IRequestHandler<TRequest, TKey>
        where TRequest : IUpdateEntityCommand<TKey>
        where TEntity : BaseEntity<TKey>
        where TRepository : IRepositoryCrud<TEntity, TKey>
    {
        protected readonly TRepository _repository;
        protected UpdateEntityCommandHandler(TRepository repository)
        {
            _repository = repository;
        }
        public async Task<TKey> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var entity = GetEntityToUpdate(request);
            await _repository.UpdateAsync(entity);
            await _repository.SaveChanges();
            return entity.Id;
        }
        protected abstract TEntity GetEntityToUpdate(TRequest request);
    }
}