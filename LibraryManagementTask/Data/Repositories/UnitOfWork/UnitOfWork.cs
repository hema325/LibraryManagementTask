using LibraryManagementTask.Data.Repositories._BooksRepo;
using LibraryManagementTask.Data.Repositories._BorrowingRecordsRepo;
using LibraryManagementTask.Data.Repositories._PatronsRepo;
using LibraryManagementTask.Data.Repositories._UsersRepo;

namespace LibraryManagementTask.Data.Repositories._UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Books = new BooksRepo(context);
            Patrons = new PatronsRepo(context);
            BorrowingRecords = new BorrowingRecordsRepo(context);
            Users = new UsersRepo(context);
        }

        public IUsersRepo Users { get; }
        public IBooksRepo Books { get; }
        public IPatronsRepo Patrons { get; }
        public IBorrowingRecordsRepo BorrowingRecords { get; }

        public async Task CompleteAsync()
            => await _context.SaveChangesAsync();

    }
}
