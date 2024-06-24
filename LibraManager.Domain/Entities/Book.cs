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

        private Book(int id, string title, DateTime createdDate, int authorId)
        {
            Id = id;
            Title = title;
            CreatedDate = createdDate;
            AuthorId = authorId;
        }

        public int Id { get; }
        public string Title { get; } = string.Empty;
        public DateTime CreatedDate { get; }

        public int AuthorId { get; }
        public virtual Author? Author { get; }
        public virtual ICollection<Genre> Genres { get; } = [];

        public static Result<Book> Create(int id, string title,  DateTime createdDate, int authorId)
        {
            if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGTH)
            {
                return Result.Failure<Book>($"'{nameof(title)}' cannot be null or empty");
            }

            if(createdDate > DateTime.Now)
            {
                return Result.Failure<Book>($"'{nameof(createdDate)}' cannot be more than DateTime.Now");
            }

            if(authorId <= 0)
            {
                return Result.Failure<Book>($"'{nameof(authorId)}' cannot be less than 1");
            }

            var book = new Book(id, title, createdDate, authorId);

            return Result.Success<Book>(book);
        }
    }
}
