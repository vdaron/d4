using System;
using d4.Core.Kernel;
using d4.Core.Kernel.Interfaces;

namespace d4.Sample.Domain.Employee
{
    public class Employee :Entity<Guid>, IAggregateRoot
    {
        
    }
}