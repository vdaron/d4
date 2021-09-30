using Ardalis.GuardClauses;
using d4.Core.Kernel;

namespace d4.Sample.Domain.Projects
{
    public record ProjectName : ValueObject
    {
        public string Value { get; }
        
        public ProjectName(string name)
        {
            Value = Guard.Against.Length(name, nameof(name),2,100);
        }
    }
}