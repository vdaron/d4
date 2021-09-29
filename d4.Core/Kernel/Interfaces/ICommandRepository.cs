using System.Collections.Generic;
using System.Threading.Tasks;

namespace d4.Core.Kernel.Interfaces
{
    public interface ICommandRepository<T, U> where T : Entity<U>, IAggregateRoot
    {
        Task<T> GetByIdAsync(U id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}