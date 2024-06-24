using CSharpFunctionalExtensions;
using LibraManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraManager.Domain.Entities
{
    public class Loan
    {
        private ICollection<Fine> _fines = [];
        private ICollection<Notification> _notifications = [];

        private Loan(DateTime? issueDate, DateTime? returnDate, Status status, int userId, int bookId)
        {
            IssueDate = issueDate;
            ReturnDate = returnDate;
            Status = status;
            UserId = userId;
            BookId = bookId;
        }

        public int id { get; }
        public DateTime? IssueDate { get; } = null;
        public DateTime? ReturnDate { get; } = null;
        public Status Status { get; private set; }

        public int UserId { get; }
        public virtual User? User { get; }
        public int BookId { get; }
        public virtual Book? Book { get; }

        public IReadOnlyCollection<Fine> FineList => _fines.ToList().AsReadOnly();
        public IReadOnlyCollection<Notification> NotificationList => _notifications.ToList().AsReadOnly();

        public void AddFine(Fine fine) => _fines.Add(fine);
        public void AddNotification(Notification notification) => _notifications.Add(notification);
        public void UpdateStatus(string value) => Status = Status.Update(value).Value;


        public static Result<Loan> Create(Status status, int userId, int bookId, DateTime? issueDate = null, DateTime? returnDate = null)
        {
            if (issueDate.Value > DateTime.Now)
                return Result.Failure<Loan>($"{nameof(issueDate)} cannot be more than DateTime.Now");

            if (returnDate.Value < issueDate.Value)
                return Result.Failure<Loan>($"{nameof(returnDate)} cannot be less than issueDate");

            var loan = new Loan(issueDate, returnDate, status, userId, bookId);

            return Result.Success(loan);
        }

    }
}
