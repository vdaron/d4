using System.Collections.Generic;
using System.Threading.Tasks;
using d4.Core.Kernel.Interfaces;
using MediatR;

namespace d4.Core.Kernel
{
    internal class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;

        public DomainEventDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Dispatch(IEnumerable<BaseDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
        }
    }
}