using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using d4.Core.Kernel;
using d4.Core.Kernel.Interfaces;
using d4.Sample.Domain.Projects;
using Microsoft.EntityFrameworkCore;

namespace d4.Sample.Infrastructure.EFCore
{
    public sealed class ProjectRepository : CommandRepositoryBase<Project,string>, IProjectRepository
    {
        private readonly ISpecificationEvaluator _specificationEvaluator;
        private readonly SampleContext _context;

        public ProjectRepository(
            ISpecificationEvaluator specificationEvaluator,
            SampleContext context, 
            IDomainEventDispatcher domainEventDispatcher) : base(domainEventDispatcher)
        {
            _specificationEvaluator = specificationEvaluator;
            _context = context;
        }

        public override async Task<Project> GetByIdAsync(string id)
        {
            return await _context.Projects.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        protected override async Task<Project> InternalAddAsync(Project entity)
        {
            _context.Add(entity);
            return entity;
        }

        protected override async Task<Project> InternalUpdateAsync(Project entity)
        {
            _context.Update(entity);
            return entity;
        }

        protected override async Task InternalDeleteAsync(Project entity)
        {
            _context.Remove(entity);
        }

        public override IUnitOfWork UnitOfWork => _context;

        public async Task<Project> GetById(string id)
        {
            return await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Project[]> ListAsync()
        {
            return await _context.Projects.ToArrayAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Projects.CountAsync();
        }

        public async Task<Project[]> ListAsync(ISpecification<Project> spec)
        {
            return await ApplySpecification(spec).ToArrayAsync();
        }

        public async Task<int> CountAsync(ISpecification<Project> spec)
        {
            return await ApplySpecification(spec,true).CountAsync();
        }

        public async Task<Project?> SingleOrDefault(ISpecification<Project> spec)
        {
            return await ApplySpecification(spec).SingleOrDefaultAsync();
        }

        /// <summary>
        /// Filters the entities  of <typeparamref name="T"/>, to those that match the encapsulated query logic of the
        /// <paramref name="specification"/>.
        /// </summary>
        /// <param name="specification">The encapsulated query logic.</param>
        /// <param name="evaluateCriteriaOnly">boolean to specify if skip,take and order by must be excluded (useful for count queries)</param>
        /// <returns>The filtered entities as an <see cref="IQueryable{T}"/>.</returns>
        private IQueryable<Project> ApplySpecification(ISpecification<Project> specification, bool evaluateCriteriaOnly = false)
        {
            return _specificationEvaluator.GetQuery(_context.Set<Project>().AsQueryable(), specification, evaluateCriteriaOnly);
        }
    }
}