using CSharpFunctionalExtensions;

namespace LibraManager.Domain.Entities
{
    public class Genre
    {
        public const int MAX_GENRE_LENGTH = 100;

        private Genre(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; } = string.Empty;
        public virtual ICollection<Book> Books { get; } = [];

        public static Result<Genre> Create(int id, string name)
        {
            if(string.IsNullOrEmpty(name) || name.Length > MAX_GENRE_LENGTH)
            {
                return Result.Failure<Genre>($"'{nameof(name)}' cannot be null or empty");
            }

            var genre = new Genre(id, name);

            return Result.Success<Genre>(genre);
        }
    }
}