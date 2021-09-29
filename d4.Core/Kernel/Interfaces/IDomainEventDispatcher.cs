using System.Collections.Generic;
using System.Threading.Tasks;

namespace d4.Core.Kernel.Interfaces
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IEnumerable<BaseDomainEvent> domainEvent);
    }
}