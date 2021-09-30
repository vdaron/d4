using Ardalis.GuardClauses;
using d4.Core.Kernel;

namespace d4.Sample.Domain.Employee
{
    public record Trigram : ValueObject
    {
        public string Value { get; set; }

        public Trigram(string value)
        {
            Value = Guard.Against.Length(value, nameof(value), 3, 3);
        }
    }
}