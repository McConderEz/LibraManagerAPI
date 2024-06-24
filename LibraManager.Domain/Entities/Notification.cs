using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraManager.Domain.Entities
{
    public class Notification
    {
        public const int TITLE_MAX_LENGTH = 50;
        public const int MESSAGE_MAX_LENGTH = 250;

        private Notification(string title,string message, DateTime sentDate, int loanId)
        {
            Title = title;
            Message = message;
            SentDate = sentDate;
            LoanId = loanId;
        }

        public int Id { get; }
        public string Title { get; }
        public string Message { get; }
        public DateTime SentDate { get; } 

        public int LoanId { get; }
        public virtual Loan? Loan { get; }

        public static Result<Notification> Create(string title, string message, DateTime sentDate, int loanId)
        {            

            if (sentDate > DateTime.Now)
                return Result.Failure<Notification>($"{nameof(sentDate)} cannot be more than DateTime.Now");

            if (string.IsNullOrWhiteSpace(title) || title.Length > TITLE_MAX_LENGTH)
                return Result.Failure<Notification>($"{nameof(title)} cannot be more than TITLE_MAX_LENGTH(50)");

            if (string.IsNullOrWhiteSpace(message) || message.Length > MESSAGE_MAX_LENGTH)
                return Result.Failure<Notification>($"{nameof(message)} cannot be more than MESSAGE_MAX_LENGTH(250)");

            var notification = new Notification(title, message, sentDate, loanId);

            return Result.Success(notification);
        }
    }
}
