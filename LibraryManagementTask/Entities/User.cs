using LibraryManagementTask.Enums;

namespace LibraryManagementTask.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public Roles Role { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
