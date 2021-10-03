using System;
using System.Linq;
using System.Threading.Tasks;
using d4.Core;
using d4.Sample.Domain;
using d4.Sample.Domain.Employee;
using d4.Sample.Domain.Projects.Commands;
using d4.Sample.Domain.Projects.Queries;
using d4.Sample.Infrastructure;
using d4.Sample.Infrastructure.EFCore;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace d4.Sample.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var provider = new ServiceCollection()
                .Addd4()
                .AddSampleDomain()
                //.AddSampleInfrastructure()
                .AddSampleInfrastructureEfCode()
                .BuildServiceProvider();

            var context = provider.GetService<SampleContext>();
            await context.Database.EnsureCreatedAsync();

            var mediator = provider.GetService<IMediator>();
            
            await mediator.Send(new CreateProjectCommand("1A"));
            await mediator.Send(new CreateProjectCommand("1C"));
            await mediator.Send(new CreateProjectCommand("1B"));
            await mediator.Send(new CreateProjectCommand("1Z"));
            await mediator.Send(new CreateProjectCommand("1AA"));

            var result = await mediator.Send(new GetAllProjectsQuery(OrderBy: x => x.Id));
            var result2 = await mediator.Send(new GetApprovedProjectsQuery(OrderBy: x => x.Id));
            var p1 = await mediator.Send(new GetProjectByNameQuery("1B"));
            
            await mediator.Send(new ApproveProjectCommand(p1.Id));

            var result3 = await mediator.Send(new GetApprovedProjectsQuery(OrderBy: x => x.Id));

            await mediator.Send(new RenameProjectCommand(p1.Id, "New Name for approved"));

            var e = Employee.Create(new Trigram("123"), "erer", "erer", DateTime.Today);

            System.Console.WriteLine("Done !");
        }
    }
}