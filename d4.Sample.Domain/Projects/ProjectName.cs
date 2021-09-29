using Ardalis.GuardClauses;
using d4.Core.Kernel;

namespace d4.Core
{
    public record ProjectName : ValueObject
    {
        public string Value { get; }
        
        public ProjectName(string name)
        {
            Value = Guard.Against.NullOrWhiteSpace(name, nameof(name));;
        }
    }
}