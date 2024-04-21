using LibraryManagementTask.Enums;
using LibraryManagementTask.Validations;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementTask.Dtos.Users
{
    public class CreateUserDto
    {
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MinLength(6)]
        public string Password { get; set; }

        [EnumType(typeof(Roles))]
        public string Role { get; set; }
    }
}
