using System;
using System.Data.Common;
using d4.Core;
using d4.Core.Kernel;
using d4.Core.Kernel.Interfaces;

namespace d4.Sample.Domain.Projects
{
    internal record ProjectCreatedEvent(string ProjectId) : BaseDomainEvent(DateTime.UtcNow);
    internal record ProjectRenamedEvent(string ProjectId) : BaseDomainEvent(DateTime.UtcNow);
    internal record ProjectApprovedEvent(string ProjectId) : BaseDomainEvent(DateTime.UtcNow);
    
    public class Project : Entity<string>, IAggregateRoot
    {
        public ProjectName Name { get; private set; }
        public DateTime CreationDate { get; private set; }
        
        public bool Approved { get; private set; }

        public static Project Create(ProjectName name)
        {
            var p = new Project(Guid.NewGuid().ToString("N"), name);
            p.PublishEvent(new ProjectCreatedEvent(p.Id));
            return p;
        }

        private Project(string id, ProjectName name)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            CreationDate = DateTime.UtcNow;
        }

        public void Rename(ProjectName name)
        {
            Name = name;
            PublishEvent(new ProjectRenamedEvent(Id));
        }

        public void Approve()
        {
            Approved = true;
            PublishEvent(new ProjectApprovedEvent(Id));
        }
    }
}