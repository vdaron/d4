using System.Threading;
using System.Threading.Tasks;
using d4.Core;
using d4.Core.Kernel.Interfaces;
using FluentValidation;
using MediatR;

namespace d4.Sample.Domain.Projects.Commands
{
    public record ApproveProjectCommand(string ProjectId) : IRequest;
    
    public class ApproveProjectCommandValidator : AbstractValidator<ApproveProjectCommand>
    {
        public ApproveProjectCommandValidator()
        {
            RuleFor(x => x.ProjectId)
                .NotEmpty().WithMessage("ProjectId cannot be empty");
        }
    }
    
    internal class ApproveProjectCommandHandler : IRequestHandler<ApproveProjectCommand>
    {
        private readonly IProjectRepository _projectCommandRepository;

        public ApproveProjectCommandHandler(IProjectRepository projectCommandRepository)
        {
            _projectCommandRepository = projectCommandRepository;
        }
        
        public async Task<Unit> Handle(ApproveProjectCommand request, CancellationToken cancellationToken)
        {
            var p = await _projectCommandRepository.GetByIdAsync(request.ProjectId);
            p.Approve();
            await _projectCommandRepository.UpdateAsync(p);
            return Unit.Value;
        }
    }
}