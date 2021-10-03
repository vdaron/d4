using System.Threading;
using System.Threading.Tasks;
using d4.Core;
using d4.Core.Kernel.Interfaces;
using FluentValidation;
using MediatR;

namespace d4.Sample.Domain.Projects.Commands
{
    public record RenameProjectCommand(string ProjectId, string NewName) : IRequest;
    
    public class RenameProjectCommandValidator : AbstractValidator<RenameProjectCommand>
    {
        public RenameProjectCommandValidator()
        {
            RuleFor(x => x.ProjectId)
                .NotEmpty().WithMessage("ProjectId cannot be empty");
            RuleFor(x => x.NewName)
                .NotEmpty().WithMessage("Username cannot be empty");
        }
    }
    
    internal class RenameProjectCommandHandler : IRequestHandler<RenameProjectCommand>
    {
        private readonly IProjectRepository _projectCommandRepository;

        public RenameProjectCommandHandler(IProjectRepository projectCommandRepository)
        {
            _projectCommandRepository = projectCommandRepository;
        }
        
        public async Task<Unit> Handle(RenameProjectCommand request, CancellationToken cancellationToken)
        {
            var p = await _projectCommandRepository.GetByIdAsync(request.ProjectId);
            p.Rename(new ProjectName(request.NewName));
            await _projectCommandRepository.UpdateAsync(p);
            return Unit.Value;
        }
    }
}