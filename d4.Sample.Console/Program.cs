using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using d4.Core.Kernel;
using d4.Core.Kernel.Interfaces;
using d4.Sample.Domain.Projects;
using d4.Sample.Domain.Projects.Commands;
using d4.Sample.Domain.Projects.Queries;
using d4.Sample.Infrastructure.Projects;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace d4.Sample.Console
{
    class Program
    {


        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddSingleton<ICommandRepository<Project, string>, ProjectsCommandRepository>();
            services.AddSingleton<IQueryableStore<Project>>(x => (IQueryableStore<Project>) x.GetService<ICommandRepository<Project, string>>());
            services.AddValidatorsFromAssembly(typeof(Project).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(Project).GetTypeInfo().Assembly, typeof(Program).GetTypeInfo().Assembly);
            var provider = services.BuildServiceProvider();

            var mediator = provider.GetService<IMediator>();
            
            await mediator.Send(new CreateProjectCommand("A"));
            await mediator.Send(new CreateProjectCommand("C"));
            await mediator.Send(new CreateProjectCommand("B"));
            await mediator.Send(new CreateProjectCommand("Z"));
            await mediator.Send(new CreateProjectCommand("AA"));

            var result = await mediator.Send(new GetAllProjectsQuery(OrderBy: x => x.Id));
            var result2 = await mediator.Send(new GetApprovedProjectsQuery(OrderBy: x => x.Id));

            await mediator.Send(new ApproveProjectCommand(result.First().Id));

            var result3 = await mediator.Send(new GetApprovedProjectsQuery(OrderBy: x => x.Id));

            await mediator.Send(new RenameProjectCommand(result.First().Id, "New Name for approved"));
            
            System.Console.WriteLine("Done !");
        }
    }
}