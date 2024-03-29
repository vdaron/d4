using System.Threading.Tasks;
using d4.Core.Kernel.Interfaces;

namespace d4.Sample.Infrastructure.InMemory.Projects
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        public Task Commit()
        {
            return Task.CompletedTask;
        }
    }
}