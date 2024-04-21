using LibraryManagementTask.Enums;
using LibraryManagementTask.Validations;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementTask.Dtos.Users
{
    public class UpdateUserDto
    {
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [EnumType(typeof(Roles))]
        public string Role { get; set; }
    }
}
