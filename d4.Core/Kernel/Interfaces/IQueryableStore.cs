using System.Threading.Tasks;
using Ardalis.Specification;

namespace d4.Core.Kernel.Interfaces
{
    public interface IQueryableStore<T, U> where T : Entity<U>
    {
        Task<T> GetById(U id);
        Task<T[]> ListAsync();
        Task<int> CountAsync();
        Task<T[]> ListAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        Task<T?> SingleOrDefault(ISpecification<T> spec);
    }
}