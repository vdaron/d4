using System;
using Ardalis.GuardClauses;
using d4.Core.Kernel;
using d4.Core.Kernel.Interfaces;

namespace d4.Sample.Domain.Employees
{
    public record EmployeeCreated(string Trigram) : BaseDomainEvent(DateTime.UtcNow);
    
    public class Employee : Entity<Trigram>, IAggregateRoot
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTimeOffset? BirthDate { get; private set; }
        public Address? Address { get; private set; }

        private Employee()
        {}
        
        public static Employee Create(Trigram trigram, string firstName, string lastName, DateTimeOffset? birthDate, Address? address=null)
        {
            var e = new Employee(trigram, firstName, lastName, birthDate, address);
            e.AddEvent(new EmployeeCreated(trigram.Value));
            return e;
        }
        
        private Employee(Trigram id, string firstName, string lastName, DateTimeOffset? birthDate, Address? address=null)
        {
            Id = id;
            FirstName = Guard.Against.NullOrWhiteSpace(firstName,nameof(firstName));
            LastName = Guard.Against.NullOrWhiteSpace(lastName,nameof(lastName));
            BirthDate = birthDate;
        }

        public void ChangeAddress(Address newAddress)
        {
            Address = newAddress;
        }
        public void ClearAddress()
        {
            Address = null;
        }
    }
}