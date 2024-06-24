using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraManager.Domain.Entities
{
    public class Fine
    {
        public const int AMOUNT_MIN = 1;
        public const int AMOUNT_MAX = 1000000;
        public const int REASON_MAX_LENGTH = 250;

        private Fine(decimal amount, DateTime issueDate, string reason, int loanId)
        {
            Amount = amount;
            IssueDate = issueDate;
            Reason = reason;
            LoanId = loanId;
        }

        public int Id { get; }
        public decimal Amount { get; }
        public DateTime IssueDate { get; }
        public string Reason { get; } //Возможно переделать в ValueObject

        public int LoanId { get; }
        public virtual Loan? Loan { get; }

        public static Result<Fine> Create(decimal amount, DateTime issueDate, string reason, int loanId)
        {
            if (amount < AMOUNT_MIN || amount > AMOUNT_MAX)
                return Result.Failure<Fine>($"{nameof(amount)} must be {AMOUNT_MIN} <= amount <= {AMOUNT_MAX}");

            if (issueDate > DateTime.Now)
                return Result.Failure<Fine>($"{nameof(issueDate)} cannot be more than DateTime.Now");

            if(string.IsNullOrWhiteSpace(reason) || reason.Length > REASON_MAX_LENGTH)
                return Result.Failure<Fine>($"{nameof(reason)} cannot be more than REASON_MAX_LENGTH(250)");

            var fine = new Fine(amount, issueDate, reason, loanId);

            return Result.Success(fine);
        }
    }
}
