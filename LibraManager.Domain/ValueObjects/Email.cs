using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LibraManager.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        private static readonly Regex ValidationRegex = new Regex(
                            @"^[\w-\.]{1,40}@([\w-]+\.)+[\w-]{2,4}$",
                            RegexOptions.Singleline | RegexOptions.Compiled);

        public string ElectronicEmail { get; }

        private Email(string email)
        {
            ElectronicEmail = email;
        }

        public static Result<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !ValidationRegex.IsMatch(email))
                return Result.Failure<Email>($"{nameof(email)} incorrect format");

            var eMail = new Email(email);

            return Result.Success(eMail);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ElectronicEmail;
        }
    }
}
