using System;
using System.Collections.Generic;
using d4.Core.Kernel.Interfaces;

namespace d4.Core.Kernel
{
    public abstract class Entity<T> : IDomainEventPublisher
    {
        protected Entity(T id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        public T Id { get; protected set; }

        private readonly List<BaseDomainEvent> _events = new List<BaseDomainEvent>();

        protected void AddEvent(BaseDomainEvent domainEvent)
        {
            _events.Add(domainEvent);
        }

        IEnumerable<BaseDomainEvent> IDomainEventPublisher.GetDomainEvents()
        {
            return _events;
        }

        void IDomainEventPublisher.ClearEvents()
        {
            _events.Clear();
        }
    }
}