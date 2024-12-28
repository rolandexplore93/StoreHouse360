using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.Common
{


    public class DeleteEntityCommand<TKey> : IRequest
    {
        public TKey key;
    }

    public abstract class DeleteEntityCommandHandler<TRequest, TEntity, TKey, TRepository> : IRequestHandler<TRequest>
        where TRequest : DeleteEntityCommand<TKey>
        where TEntity : BaseEntity<TKey>
        where TRepository : IRepositoryCrud<TEntity, TKey>
    {
        private readonly TRepository _repository;

        protected DeleteEntityCommandHandler(TRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(TRequest request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.key);
            await _repository.SaveChanges();
        }
    }
}
