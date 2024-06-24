using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace LibraManager.Domain.ValueObjects
{
    public class Name : ValueObject
    {

        private static readonly Regex ValidationRegex = new Regex(
                            @"^[\p{L}\p{M}\p{N}]{1,50}\z",
                            RegexOptions.Singleline | RegexOptions.Compiled);

        public string FirstName { get; }
        public string SecondName { get; }
        public string MiddleName { get; } 

        private Name(string firstName, string secondName, string middleName)
        {
            FirstName = firstName;
            SecondName = secondName;
            MiddleName = middleName;
        }

        public static Result<Name> Create(string firstName,string secondName, string middleName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || !ValidationRegex.IsMatch(firstName))
                return Result.Failure<Name>($"{nameof(firstName)} incorrect format");

            if (string.IsNullOrWhiteSpace(secondName) || !ValidationRegex.IsMatch(secondName))
                return Result.Failure<Name>($"{nameof(secondName)} incorrect format");

            if (string.IsNullOrWhiteSpace(middleName) || !ValidationRegex.IsMatch(middleName))
                return Result.Failure<Name>($"{nameof(middleName)} incorrect format");

            var name = new Name(firstName, secondName, middleName);

            return Result.Success(name);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return SecondName;
            yield return MiddleName;
        }
    }
}
