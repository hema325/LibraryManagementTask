namespace LibraryManagementTask.Entities
{
    public class BorrowingRecord
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int PatronId { get; set; }
        public DateTime DateBorrowed { get; set; }
        public DateTime? DateReturned { get; set; }
        public string? Notes { get; set; }

        //navigations
        public Book Book { get; set; }
        public Patron Patron { get; set; }
    }
}
