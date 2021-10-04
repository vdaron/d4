using System;
using AutoMapper;
using d4.Sample.Domain.Employees;

namespace d4.Sample.Infrastructure.JsonFiles
{
    internal record EmployeeState(
        string Trigram,
        string FirstName,
        string LastName,
        DateTimeOffset? BirthDate,
        Address? Address);

    public class EmployeeStateProfile : Profile
    {
        public EmployeeStateProfile()
        {
            CreateMap<EmployeeState, Employee>()
                .ForMember(x => 
                        x.Id,
                    x => x.MapFrom(y => new Trigram(y.Trigram)));
        }
    }
}