using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using d4.Core.Kernel.Interfaces;
using d4.Sample.Domain.Projects;
using d4.Sample.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace d4.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSampleInfrastructureEfCode(this IServiceCollection services)
        {

            services.AddDbContext<SampleContext>(options =>
            {
                options.UseSqlite("DataSource=/tmp/test");
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }, 
                // Default is Scoped, but we want to be sure that each UnitOfWork have a new DbContext
                ServiceLifetime.Transient);
            
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IQueryableStore<Project,string>, ProjectRepository>();
            services.AddScoped(c =>
                (ICommandRepository<Project, string>) c.GetService<IQueryableStore<Project, string>>());
            services.AddSingleton<ISpecificationEvaluator>(x => SpecificationEvaluator.Default);
            return services;
        }
    }
}