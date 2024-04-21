﻿namespace LibraryManagementTask.Dtos.Books
{
    public class CreateOrUpdateBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public string? Notes { get; set; }
    }
}
