using System.Reflection;
using d4.Core.Kernel;
using d4.Core.Kernel.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace d4.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Addd4(this IServiceCollection services)
        {
            services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}