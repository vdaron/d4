using System.Threading.Tasks;
using d4.Core.Kernel.Interfaces;

namespace d4.Core.Kernel
{
    public abstract class CommandRepositoryBase<T,U> : ICommandRepository<T,U> where T : Entity<U>, IAggregateRoot
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public CommandRepositoryBase(IDomainEventDispatcher domainEventDispatcher)
        {
            _domainEventDispatcher = domainEventDispatcher;
        }

        public abstract Task<T?> GetByIdAsync(U id);
        protected abstract Task<T> InternalAddAsync(T entity);
        protected abstract Task<T> InternalUpdateAsync(T entity);
        protected abstract Task InternalDeleteAsync(T entity);
        public abstract IUnitOfWork UnitOfWork{ get; }

        public async Task<T> CreateAsync(T entity)
        {
            await InternalAddAsync(entity);
            await DispatchEvents(entity);
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            await InternalUpdateAsync(entity);
            await DispatchEvents(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            await InternalDeleteAsync(entity);
            await DispatchEvents(entity);
        }

        private async Task DispatchEvents(T entity)
        {
            if (entity is IDomainEventPublisher eventPublisher)
            {
                await _domainEventDispatcher.Dispatch(eventPublisher.GetDomainEvents());
                eventPublisher.ClearEvents();
            }
        }
    }
}