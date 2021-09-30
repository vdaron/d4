using Ardalis.GuardClauses;
using d4.Core.Kernel;

namespace d4.Sample.Domain.Employee
{
    public record Address : ValueObject
    {
        public Address(string street, int number, int? box, int postCode, string city, string country)
        {
            Street = Guard.Against.NullOrWhiteSpace(street,nameof(street));
            Number = Guard.Against.NegativeOrZero(number,nameof(number));
            Box = box;
            PostCode = postCode;
            City = Guard.Against.NullOrWhiteSpace(city,nameof(city));
            Country = Guard.Against.NullOrWhiteSpace(country,nameof(country));
        }

        public string Street { get; init; }
        public int Number { get; init; }
        public int? Box { get; init; }
        public int PostCode { get; init; }
        public string City { get; init; }
        public string Country { get; init; }
    }
}