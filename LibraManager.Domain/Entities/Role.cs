using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraManager.Domain.Entities
{
    public class Role
    {
        public const int MAX_NAME_SIZE = 50; 

        private Role(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; } = string.Empty;

        public Result<Role> Create(int id, string name)
        {
            if(string.IsNullOrWhiteSpace(name) && name.Length < MAX_NAME_SIZE)
            {
                return Result.Failure<Role>($"{nameof(name)} cannot be null!");
            }

            var role = new Role(id, name);

            return Result.Success(role);
        }
        
    }
}
