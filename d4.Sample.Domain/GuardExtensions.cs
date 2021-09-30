using System;
using Ardalis.GuardClauses;

namespace d4.Sample.Domain
{
    public static class GuardExtensions
    {
        public static string Length(this IGuardClause against,string input, string parameterName, int min, int max)
        {
            Guard.Against.NullOrWhiteSpace(input, parameterName);
            
            if (input.Length < min || input.Length > max)
            {
                throw new ArgumentException($"Parameter name '{parameterName}' length is incorrect : {input.Length} (min : {min}, max : {max})");
            }

            return input;
        }
    }
}