using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraManager.Domain.ValueObjects
{
    public class Status : ValueObject
    {
        public static readonly Status Available = new(nameof(Available));
        public static readonly Status Taken = new(nameof(Taken));

        private static readonly Status[] _all = [Available, Taken];

        public string Value { get; }

        private Status(string value)
        {
            Value = value;
        }

        public static Result<Status> Update(string newValue)
        {
            var result = Create(newValue);

            if (result.IsSuccess)
            {
                return result.Value;
            }

            return result;
        }

        public static Result<Status> Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Result.Failure<Status>($"{nameof(input)} cannot be null");

            var status = input.Trim().ToLower();

            if(_all.Any(s => s.Value.ToLower() == status) == false)
            {
                return Result.Failure<Status>($"{status} is not correct");
            }

            return new Status(input);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
