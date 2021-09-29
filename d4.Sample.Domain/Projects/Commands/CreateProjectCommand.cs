using System.Threading;
using System.Threading.Tasks;
using d4.Core;
using d4.Core.Kernel.Interfaces;
using FluentValidation;
using MediatR;

namespace d4.Sample.Domain.Projects.Commands
{
    public record CreateProjectCommand(string Name) : IRequest;

    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(x => x.Name)
                .Length(1, 100).WithMessage("Name length must be between 2 and 10 characters")
                .NotEmpty().WithMessage("Username cannot be empty");
        }
    }

    internal class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand>
    {
        private readonly ICommandRepository<Project,string> _projectCommandRepository;

        public CreateProjectCommandHandler(ICommandRepository<Project,string> projectCommandRepository)
        {
            _projectCommandRepository = projectCommandRepository;
        }
        
        public async Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var p = Project.Create(new ProjectName(request.Name));
            await _projectCommandRepository.AddAsync(p);
            return Unit.Value;
        }
    }
}