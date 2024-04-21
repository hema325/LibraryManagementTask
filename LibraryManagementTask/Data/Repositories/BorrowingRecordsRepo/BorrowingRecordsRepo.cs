using LibraryManagementTask.Data.Repositories._BaseRepo;
using LibraryManagementTask.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementTask.Data.Repositories._BorrowingRecordsRepo

{
    public class BorrowingRecordsRepo : BaseRepo<BorrowingRecord>, IBorrowingRecordsRepo
    {
        public BorrowingRecordsRepo(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<BorrowingRecord?> GetBorrowedBookByPatronIdBookIdAsync(int patronId, int bookId)
        {
            return await _context.BorrowingRecords.FirstOrDefaultAsync(r => r.PatronId == patronId &&
            r.BookId == bookId &&
            r.DateReturned == null);
        }
    }
}
