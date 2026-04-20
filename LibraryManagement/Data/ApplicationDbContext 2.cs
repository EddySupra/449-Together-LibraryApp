using Microsoft.EntityFrameworkCore;
using LibraryManagement.Api.Models;

namespace LibraryManagement.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Member> Members => Set<Member>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<BorrowRecord> BorrowRecords => Set<BorrowRecord>();
    }
}
