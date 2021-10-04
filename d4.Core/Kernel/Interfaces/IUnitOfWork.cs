using System.Threading.Tasks;

namespace d4.Core.Kernel.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}