using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Common
{
    public interface ICreateEntityCommand<out TKey> : IRequest<TKey>
    {

    }
    public abstract class CreateEntityCommand<TRequest, TEntity, TKey, TRepository> : IRequestHandler<TRequest, TKey>
        where TRequest : ICreateEntityCommand<TKey>
        where TEntity : BaseEntity<TKey>
        where TRepository : IRepositoryCrud<TEntity, TKey>
    {
        protected readonly TRepository _repository;

        public CreateEntityCommand(TRepository repository)
        {
            _repository = repository;
        }

        public async Task<TKey> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var entity = CreateEntity(request);
            entity = await _repository.CreateAsync(entity);
            await _repository.SaveChanges();
            return entity.Id;
        }

        protected abstract TEntity CreateEntity(TRequest request);
    }
}
