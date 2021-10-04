using d4.Core.Kernel.Interfaces;

namespace d4.Sample.Domain.Employees
{
    public interface IEmployeeRepository : ICommandRepository<Employees.Employee, Trigram>
    {
    }
}