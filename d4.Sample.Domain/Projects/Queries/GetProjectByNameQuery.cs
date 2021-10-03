using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using d4.Core.Kernel;
using d4.Core.Kernel.Interfaces;
using JetBrains.Annotations;
using MediatR;

namespace d4.Sample.Domain.Projects.Queries
{
    public record GetProjectByNameQuery(string name) : IRequest<Project?>;

    public class GetProjectByNameQuerySpecification : Specification<Project>
    {
        public GetProjectByNameQuerySpecification(string name)
        {
            Query.Where(x => x.Name == new ProjectName(name));
            Query.AsNoTracking();
        }
    }
    internal class GetProjectByNameQueryHandler : IRequestHandler<GetProjectByNameQuery, Project?>
    {

        
        private readonly IQueryableStore<Project,string> _repository;

        public GetProjectByNameQueryHandler(IQueryableStore<Project,string> repository)
        {
            _repository = repository;
        }
        
        public async Task<Project?> Handle(GetProjectByNameQuery query, CancellationToken cancellationToken)
        {
            return await _repository.SingleOrDefault(new GetProjectByNameQuerySpecification(query.name));
        }
    }
}