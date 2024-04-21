using LibraryManagementTask.Data.Repositories._BaseRepo;
using LibraryManagementTask.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementTask.Data.Repositories._UsersRepo
{
    public class UsersRepo : BaseRepo<User>, IUsersRepo
    {
        public UsersRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByEmailAsync(string email)
            => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}
