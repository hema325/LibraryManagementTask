using LibraryManagementTask.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementTask.Data.Configurations
{
    public class BorrowingRecordConfig : IEntityTypeConfiguration<BorrowingRecord>
    {
        public void Configure(EntityTypeBuilder<BorrowingRecord> builder)
        {
            builder.HasOne(r => r.Book)
                .WithMany(b => b.BorrowingRecords)
                .HasForeignKey(r => r.BookId);

            builder.HasOne(r => r.Patron)
                .WithMany(b => b.BorrowingRecords)
                .HasForeignKey(r => r.PatronId);
        }
    }
}
