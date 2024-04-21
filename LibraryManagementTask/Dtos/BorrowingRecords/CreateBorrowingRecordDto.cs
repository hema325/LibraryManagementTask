namespace LibraryManagementTask.Dtos.BorrowingRecords
{
    public class CreateBorrowingRecordDto
    {
        public DateTime DateBorrowed { get; set; }
        public string? Notes { get; set; }
    }
}
