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
            });
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IQueryableStore<Project,string>, ProjectRepository>();
            services.AddSingleton<ISpecificationEvaluator>(x => SpecificationEvaluator.Default);
            return services;
        }
    }
}