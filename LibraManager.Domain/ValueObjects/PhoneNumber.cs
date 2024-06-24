﻿using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LibraManager.Domain.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        private static readonly Regex ValidationRegex = new Regex(
                            @"(^\+\d{1,3}\d{10}$|^$)",
                            RegexOptions.Singleline | RegexOptions.Compiled);

        public string Number { get; }

        private PhoneNumber(string phoneNumber)
        {
            Number = phoneNumber;
        }

        public static Result<PhoneNumber> Create(string number)
        {
            if (string.IsNullOrWhiteSpace(number) || !ValidationRegex.IsMatch(number))
                return Result.Failure<PhoneNumber>($"{nameof(number)} incorrect format");

            var phoneNumber = new PhoneNumber(number);

            return Result.Success(phoneNumber);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
