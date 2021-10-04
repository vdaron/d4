using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace d4.Sample.Domain.Employees.Commands
{
    public record CreateEmployeeCommand(
        string Trigram,
        string FirstName,
        string LastName,
        DateTimeOffset? BirthDate) : IRequest;
    
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.Trigram).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
        }
    }

    internal class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        
        public async Task<Unit> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var e = await _employeeRepository.GetByIdAsync(new Trigram(request.Trigram));
            if(e != null)
                throw new Exception("Employee Already exists");
            
            await _employeeRepository.CreateAsync(
                Employee.Create(
                    new Trigram(request.Trigram),
                    request.FirstName,
                    request.LastName,
                    request.BirthDate));
            await _employeeRepository.UnitOfWork.Commit();
            return Unit.Value;
        }
    }
}