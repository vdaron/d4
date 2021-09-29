
using d4.Sample.Domain.Projects;

namespace d4.Sample.Domain
{
    internal class SampleEventHandler : MediatR.NotificationHandler<ProjectCreatedEvent>
    {
        protected override void Handle(ProjectCreatedEvent notification)
        {
            System.Console.WriteLine("Project created !");
        }
    }
}