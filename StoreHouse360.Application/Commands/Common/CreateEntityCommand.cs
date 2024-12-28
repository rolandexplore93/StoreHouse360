using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Common
{
    public interface ICreateEntityCommand<out TKey> : IRequest<TKey>
    {

    }

    public abstract class CreateEntityCommandHandler<TRequest, TEntity, TKey, TRepository> : IRequestHandler<TRequest, TKey>
        where TRequest : ICreateEntityCommand<TKey>
        where TEntity : BaseEntity<TKey>
        where TRepository : IRepositoryCrud<TEntity, TKey>
    {
        protected readonly TRepository _repository;

        public CreateEntityCommandHandler(TRepository repository)
        {
            _repository = repository;
        }

        public async Task<TKey> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var entity = CreateEntity(request);
            var saveAction = await _repository.CreateAsync(entity);
            entity = await saveAction();
            return entity.Id;
        }

        protected abstract TEntity CreateEntity(TRequest request);
    }
}
