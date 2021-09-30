using System.Reflection;
using d4.Sample.Domain.Projects;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace d4.Sample.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSampleDomain(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(Project).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(Project).GetTypeInfo().Assembly);

            return services;
        }
    }
}