using System;
using MediatR;

namespace d4.Core.Kernel
{
    public abstract record BaseDomainEvent(DateTime DateOccurred) : INotification;
}