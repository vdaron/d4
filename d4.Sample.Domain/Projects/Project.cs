using System;
using System.Reflection.Metadata;
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
            p.AddEvent(new ProjectCreatedEvent(p.Id));
            return p;
        }

        private Project()
        {
            
        }

        private Project(string id, ProjectName name)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            CreationDate = DateTime.UtcNow;
        }

        public void Rename(ProjectName name)
        {
            Name = name;
            AddEvent(new ProjectRenamedEvent(Id));
        }

        public void Approve()
        {
            Approved = true;
            AddEvent(new ProjectApprovedEvent(Id));
        }
    }
}