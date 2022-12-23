using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using d4.Core.Kernel;
using d4.Core.Kernel.Interfaces;
using d4.Sample.Domain.Projects;
using JetBrains.Annotations;

namespace d4.Sample.Infrastructure.InMemory.Projects
{
    public class ProjectsCommandRepository : CommandRepositoryBase<Project, string>, IProjectRepository
    {
        private readonly Dictionary<string, Project> _projects = new Dictionary<string, Project>();

        public ProjectsCommandRepository(IDomainEventDispatcher domainEventDispatcher) : base(domainEventDispatcher)
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

        public Task<int> CountAsync()
        {
            return Task.FromResult(_projects.Count);
        }

        public Task<int> CountAsync(ISpecification<Project> spec)
        {
            //this is very suboptimal, and take into account paging...=> this is wrong
            return Task.FromResult(spec.Evaluate(_projects.Values).Count());
        }

        public Task<Project?> SingleOrDefault(ISpecification<Project> spec)
        {
            return Task.FromResult(spec.Evaluate(_projects.Values).SingleOrDefault());
        }

        protected override Task<Project> InternalAddAsync(Project entity)
        {
            _projects.Add(entity.Id, entity);
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

        public override IUnitOfWork UnitOfWork => new InMemoryUnitOfWork();
    }
}