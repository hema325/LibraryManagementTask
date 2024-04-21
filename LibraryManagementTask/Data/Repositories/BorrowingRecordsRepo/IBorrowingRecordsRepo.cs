using LibraryManagementTask.Data.Repositories._BaseRepo;
using LibraryManagementTask.Entities;

namespace LibraryManagementTask.Data.Repositories._BorrowingRecordsRepo
{
    public interface IBorrowingRecordsRepo : IBaseRepo<BorrowingRecord>
    {
        Task<BorrowingRecord?> GetBorrowedBookByPatronIdBookIdAsync(int patronId, int bookId);
    }
}
