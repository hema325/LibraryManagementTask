using LibraryManagementTask.Data.Repositories._BooksRepo;
using LibraryManagementTask.Data.Repositories._BorrowingRecordsRepo;
using LibraryManagementTask.Data.Repositories._PatronsRepo;
using LibraryManagementTask.Data.Repositories._UsersRepo;

namespace LibraryManagementTask.Data.Repositories._UnitOfWork
{
    public interface IUnitOfWork
    {
        IBooksRepo Books { get; }
        IPatronsRepo Patrons { get; }
        IBorrowingRecordsRepo BorrowingRecords { get; }
        IUsersRepo Users { get; }

        Task CompleteAsync();
    }
}
