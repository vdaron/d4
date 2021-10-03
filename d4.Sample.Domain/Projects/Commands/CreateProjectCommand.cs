using System;
using System.Threading;
using System.Threading.Tasks;
using d4.Core;
using d4.Core.Kernel.Interfaces;
using d4.Sample.Domain.Projects.Queries;
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
                .NotEmpty().WithMessage("Project name cannot be empty");
        }
    }

    internal class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand>
    {
        private readonly IProjectRepository _projectCommandRepository;
        private readonly IQueryableStore<Project, string> _queryableStore;

        public CreateProjectCommandHandler(
            IProjectRepository projectCommandRepository, 
            IQueryableStore<Project, string> queryableStore)
        {
            _projectCommandRepository = projectCommandRepository;
            _queryableStore = queryableStore;
        }
        
        public async Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var existing = await _queryableStore.SingleOrDefault(new GetProjectByNameQuerySpecification(request.Name));
            if (existing != null)
                throw new ApplicationException("Unable to create two project with the same name");
            
            var p = Project.Create(new ProjectName(request.Name));
            await _projectCommandRepository.CreateAsync(p);
            return Unit.Value;
        }
    }
}