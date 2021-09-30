using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using d4.Core.Kernel;
using d4.Core.Kernel.Interfaces;
using d4.Sample.Domain.Projects;

namespace d4.Sample.Infrastructure.Projects
{
    public class ProjectsCommandRepository : CommandRepositoryBase<Project,string>, IQueryableStore<Project,string>
    {
        private readonly Dictionary<string, Project> _projects = new Dictionary<string, Project>();

        public ProjectsCommandRepository(IDomainEventDispatcher domainEventDispatcher):base(domainEventDispatcher)
        {
        }
        
        public override Task<Project> GetByIdAsync(string id)
        {
            return Task.FromResult(_projects[id]);
        }

        public Task<Project> GetById(string id)
        {
            return Task.FromResult(_projects[id]);
        }

        public Task<Project[]> ListAsync()
        {
            return Task.FromResult(_projects.Values.ToArray());
        }

        public Task<Project[]> ListAsync(ISpecification<Project> spec)
        {
            return Task.FromResult(spec.Evaluate(_projects.Values).ToArray());
        }

        public Task<Project> SingleAsync(ISpecification<Project> spec)
        {
            return Task.FromResult(spec.Evaluate(_projects.Values).Single());
        }

        protected override Task<Project> InternalAddAsync(Project entity)
        {
            _projects.Add(entity.Id,entity);
            return Task.FromResult(entity);
        }

        protected override Task<Project> InternalUpdateAsync(Project entity)
        {
            _projects[entity.Id] = entity;
            return Task.FromResult(entity);
        }

        protected override Task InternalDeleteAsync(Project entity)
        {
            _projects.Remove(entity.Id);
            return Task.CompletedTask;
        }
    }
}