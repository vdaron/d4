using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using d4.Core;
using d4.Core.Kernel.Interfaces;
using d4.Sample.Domain.Projects.Queries;
using FluentValidation;
using MediatR;

namespace d4.Sample.Domain.Projects.Commands
{
    public record CreateProjectsCommand(params string[] Names) : IRequest;

    public class CreateProjectsCommandValidator : AbstractValidator<CreateProjectsCommand>
    {
        public CreateProjectsCommandValidator()
        {
            RuleForEach(x => x.Names)
                .NotEmpty().WithMessage("Project name cannot be empty");
        }
    }

    internal class CreateProjectsCommandHandler : IRequestHandler<CreateProjectsCommand>
    {
        private readonly ICommandRepository<Project, string> _commandRepository;
        private readonly IQueryableStore<Project, string> _queryableStore;

        public CreateProjectsCommandHandler(
            IQueryableStore<Project, string> queryableStore, 
            ICommandRepository<Project, string> commandRepository)
        {
            _queryableStore = queryableStore;
            _commandRepository = commandRepository;
        }
        
        public async Task<Unit> Handle(CreateProjectsCommand request, CancellationToken cancellationToken)
        {
            if (request.Names.Any())
            {
                foreach (var name in request.Names)
                {
                    var existing = await _queryableStore.SingleOrDefault(new GetProjectByNameQuerySpecification(name));
                    if (existing != null)
                        throw new ApplicationException("Unable to create two project with the same name");

                    await _commandRepository.CreateAsync(Project.Create(new ProjectName(name)));
                }

                await _commandRepository.UnitOfWork.Commit();
            }

            return Unit.Value;
        }
    }
}