using LibraryManagementTask.Data.Repositories._BaseRepo;
using LibraryManagementTask.Entities;

namespace LibraryManagementTask.Data.Repositories._BooksRepo
{
    public class BooksRepo : BaseRepo<Book>, IBooksRepo
    {
        public BooksRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
