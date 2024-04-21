using LibraryManagementTask.Data.Repositories._BaseRepo;
using LibraryManagementTask.Entities;

namespace LibraryManagementTask.Data.Repositories._UsersRepo
{
    public interface IUsersRepo : IBaseRepo<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
