using System.Threading.Tasks;
using Ardalis.Specification;

namespace d4.Core.Kernel.Interfaces
{
    public interface IQueryableStore<T>
    {
        Task<T[]> ListAsync();
        Task<T[]> ListAsync(ISpecification<T> spec);
    }
}