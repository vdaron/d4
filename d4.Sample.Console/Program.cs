using System;
using System.Threading.Tasks;
using d4.Core;
using d4.Sample.Domain;
using d4.Sample.Domain.Employees;
using d4.Sample.Domain.Employees.Commands;
using d4.Sample.Domain.Projects.Commands;
using d4.Sample.Domain.Projects.Queries;
using d4.Sample.Infrastructure.EFCore;
using d4.Sample.Infrastructure.JsonFiles;
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
                .AddSampleInfrastructureEfCode()
                .AddSampleInfrastructureJsonFiles()
                .BuildServiceProvider();

            var context = provider.GetService<SampleContext>();
            await context.Database.EnsureCreatedAsync();

            var mediator = provider.GetService<IMediator>();


            await mediator.Send(new CreateEmployeeCommand(
                "vda",
                "Vincent",
                "Daron",
                new DateTimeOffset(1977, 10, 20, 0, 0, 0,TimeSpan.Zero)));
            
            await mediator.Send(new CreateProjectsCommand("1A","1C","1B","1Z","1AA"));

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