using CSharpFunctionalExtensions;

namespace LibraManager.Domain.Entities
{
    public class Author
    {
        public const int MAX_NAMES_LENGHT = 50;
        public const int MIN_AGE = 10;
        public const int MAX_AGE = 120;

        private Author(int id, string firstName, string lastName,string middleName, int age)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Age = age;
        }

        public int Id { get; }
        public string FirstName { get; } = string.Empty;
        public string LastName { get; } = string.Empty;
        public string MiddleName { get; } = string.Empty;
        public int Age { get; }
        public virtual ICollection<Book> Books { get; } = [];

        public static Result<Author> Create(int id, string firstName,string lastName, string middleName, int age)
        {
            if (string.IsNullOrEmpty(firstName) || firstName.Length > MAX_NAMES_LENGHT)
            {
                return Result.Failure<Author>($"'{nameof(firstName)}' cannot be null or empty");
            }

            if (string.IsNullOrEmpty(lastName) || lastName.Length > MAX_NAMES_LENGHT)
            {
                return Result.Failure<Author>($"'{nameof(lastName)}' cannot be null or empty");
            }

            if (string.IsNullOrEmpty(middleName) || middleName.Length > MAX_NAMES_LENGHT)
            {
                return Result.Failure<Author>($"'{nameof(middleName)}' cannot be null or empty");
            }

            if (age >= MIN_AGE && age <= MAX_AGE)
            {
                return Result.Failure<Author>($"'{nameof(age)}' can be in {MIN_AGE} <= age <= {MAX_AGE}");
            }

            var author = new Author(id, firstName, lastName, middleName, age);

            return Result.Success<Author>(author);
        }
    }
}