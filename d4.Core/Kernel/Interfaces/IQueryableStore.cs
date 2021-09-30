using System.Threading.Tasks;
using Ardalis.Specification;

namespace d4.Core.Kernel.Interfaces
{
    public interface IQueryableStore<T, U> where T : Entity<U>
    {
        Task<T> GetById(U id);
        Task<T[]> ListAsync();
        Task<T[]> ListAsync(ISpecification<T> spec);
        Task<T> SingleAsync(ISpecification<T> spec);
    }
}