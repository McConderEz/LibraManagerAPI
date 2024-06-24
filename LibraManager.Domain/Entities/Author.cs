using CSharpFunctionalExtensions;
using LibraManager.Domain.ValueObjects;

namespace LibraManager.Domain.Entities
{
    public class Author
    {
        public const int MIN_AGE = 10;
        public const int MAX_AGE = 120;

        private Author(Name name, int age)
        {
            Name = name;
            Age = age;
        }

        public int Id { get; }
        public Name Name { get; }
        public int Age { get; }
        public virtual ICollection<Book> Books { get; } = [];

        public static Result<Author> Create(Name name, int age)
        {

            if (age < MIN_AGE && age > MAX_AGE)
            {
                return Result.Failure<Author>($"'{nameof(age)}' can be in {MIN_AGE} <= age <= {MAX_AGE}");
            }

            var author = new Author(name, age);

            return Result.Success<Author>(author);
        }
    }
}