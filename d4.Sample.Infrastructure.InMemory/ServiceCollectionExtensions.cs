using d4.Core.Kernel.Interfaces;
using d4.Sample.Domain.Projects;
using d4.Sample.Infrastructure.InMemory.Projects;
using Microsoft.Extensions.DependencyInjection;

namespace d4.Sample.Infrastructure.InMemory
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSampleInfrastructureInMemory(this IServiceCollection services)
        {
            services.AddSingleton<IQueryableStore<Project,string>, ProjectsCommandRepository>();
            //services.AddSingleton(x => (IQueryableStore<Project,string>) x.GetService<ICommandRepository<Project, string>>());
            return services;
        }
    }
}