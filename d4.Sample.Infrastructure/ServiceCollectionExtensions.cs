using d4.Core.Kernel.Interfaces;
using d4.Sample.Domain.Projects;
using d4.Sample.Infrastructure.Projects;
using Microsoft.Extensions.DependencyInjection;

namespace d4.Sample.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSampleInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<ICommandRepository<Project, string>, ProjectsCommandRepository>();
            services.AddSingleton(x => (IQueryableStore<Project,string>) x.GetService<ICommandRepository<Project, string>>());
            return services;
        }
    }
}