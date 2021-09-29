using System.Collections.Generic;

namespace d4.Core.Kernel.Interfaces
{
    public interface IDomainEventPublisher
    {
        IEnumerable<BaseDomainEvent> GetDomainEvents();
        void ClearEvents();
    }
}