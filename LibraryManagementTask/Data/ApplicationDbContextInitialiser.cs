using LibraryManagementTask.Entities;
using LibraryManagementTask.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementTask.Data
{
    public class ApplicationDbContextInitialiser
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public ApplicationDbContextInitialiser(ApplicationDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task InitialiseAsync()
        {
            await _context.Database.MigrateAsync();

            if(!await _context.Users.AnyAsync())
            {
                var user = new User
                {
                    Name = "Ibrahim Moawad",
                    Email = "admin@gmail.com",
                    Role = Roles.Admin
                };

                user.HashedPassword = _passwordHasher.HashPassword(user, "Pa$$w0rd");

                _context.Add(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
