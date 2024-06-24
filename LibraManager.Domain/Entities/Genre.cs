using CSharpFunctionalExtensions;

namespace LibraManager.Domain.Entities
{
    public class Genre
    {
        public const int MAX_GENRE_LENGTH = 100;

        private Genre(string name)
        {
            Name = name;
        }

        public int Id { get; }
        public string Name { get; } = string.Empty;

        private readonly ICollection<Book> _books = [];
        public IReadOnlyCollection<Book> BookList => _books.ToList();

        public void AddBook(Book book) => _books.Add(book);

        public static Result<Genre> Create(string name)
        {
            if(string.IsNullOrEmpty(name) || name.Length > MAX_GENRE_LENGTH)
            {
                return Result.Failure<Genre>($"'{nameof(name)}' cannot be null or empty");
            }

            var genre = new Genre(name);

            return Result.Success<Genre>(genre);
        }
    }
}