﻿namespace LibraryManagementTask.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public string? Notes { get; set; }

        //navigations
        public ICollection<BorrowingRecord> BorrowingRecords { get; set;}
    }
}