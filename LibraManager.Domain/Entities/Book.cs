using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibraManager.Domain.Entities
{
    public class Book
    {
        public const int MAX_TITLE_LENGTH = 350;

        private Book(string title, DateTime createdDate, int loanId)
        {
            Title = title;
            CreatedDate = createdDate;
            LoanId = loanId;
        }

        public int Id { get; }
        public string Title { get; } = string.Empty;
        public DateTime CreatedDate { get; }

        public int LoanId { get; }
        public virtual Loan? Loan { get; }

        private readonly ICollection<Author> _authors = [];
        private readonly ICollection<Genre> _genres = [];

        public IReadOnlyCollection<Author> AuthorList => _authors.ToList();
        public IReadOnlyCollection<Genre> GenreList => _genres.ToList();

        public void AddAuthor(Author author) => _authors.Add(author);
        public void AddGenre(Genre genre) => _genres.Add(genre);

        public static Result<Book> Create(string title,  DateTime createdDate, int loanId)
        {
            if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGTH)
            {
                return Result.Failure<Book>($"'{nameof(title)}' cannot be null or empty");
            }

            if(createdDate > DateTime.Now)
            {
                return Result.Failure<Book>($"'{nameof(createdDate)}' cannot be more than DateTime.Now");
            }

            var book = new Book(title, createdDate, loanId);

            return Result.Success<Book>(book);
        }
    }
}
