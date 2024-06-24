using CSharpFunctionalExtensions;
using LibraManager.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace LibraManager.Domain.Entities
{
    public class User
    {
        public const int MAX_NAME_SIZE = 80;

        private ICollection<Loan> _loans = [];

        private User(string name, Email email, PhoneNumber phoneNumber, Address address, int roleId)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            RoleId = roleId;
        }

        public int Id { get; }
        public string Name { get; } = string.Empty;
        public Email Email { get; } 
        public PhoneNumber PhoneNumber { get; } 
        public Address Address { get; } 


        public int RoleId { get; }
        public virtual Role? Role { get; }

        public IReadOnlyCollection<Loan> LoansList => _loans.ToList().AsReadOnly();

        public void AddLoan(Loan loan) => _loans.Add(loan);

        public static Result<User> Create(string name, Email email, PhoneNumber phoneNumber, Address address ,int roleId)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > MAX_NAME_SIZE)
                return Result.Failure<User>($"{nameof(name)} cannot be null or length be more than 80 char!");

            var user = new User(name, email, phoneNumber, address, roleId);

            return Result.Success(user);
        }
    }
}
