using System.Collections.Generic;
using System.Threading.Tasks;

namespace d4.Core.Kernel.Interfaces
{
    public interface ICommandRepository<T, U> where T : Entity<U>, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        Task<T?> GetByIdAsync(U id);
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}