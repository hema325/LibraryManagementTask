namespace LibraryManagementTask.Entities
{
    public class Patron
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactInformation { get; set; }
        public string? Notes { get; set; }

        //navigations
        public ICollection<BorrowingRecord> BorrowingRecords { get; set; }
    }
}
