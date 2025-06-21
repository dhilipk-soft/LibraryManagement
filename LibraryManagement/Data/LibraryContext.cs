using LibraryManagement.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data
{
    public class LibraryContext : DbContext
    {

        public LibraryContext(DbContextOptions options) : base (options) { }

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Category> Categoryies { get; set; } = null!;

        public DbSet<Librarian> Librarians { get; set; } = null!;
        public DbSet<Loan> Loans { get; set; } = null!;
        public DbSet<Member> Members { get; set; } = null!;
    }
}
