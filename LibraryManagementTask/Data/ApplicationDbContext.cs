using LibraryManagementTask.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LibraryManagementTask.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<User> Users { get; private set; }
        public DbSet<Book> Books { get; private set; }
        public DbSet<Patron> Patrons { get; private set; }
        public DbSet<BorrowingRecord> BorrowingRecords { get; private set; }
        
    }
}
