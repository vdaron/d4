using d4.Core.Kernel.Interfaces;

namespace d4.Sample.Domain.Projects
{
    public interface IProjectRepository : 
        ICommandRepository<Project,string>,
        IQueryableStore<Project,string>
    {
        
    }
}