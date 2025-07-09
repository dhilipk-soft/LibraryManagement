using LibraryManagement.Domain.Entities;
using LibraryManagement.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace LibraryManagement.Infrastructure
{
    public class LibraryContext : DbContext
    {

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Category> Categoryies { get; set; } = null!;

        public DbSet<Librarian> Librarians { get; set; } = null!;
        public DbSet<Loan> Loans { get; set; } = null!;
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<LibraryMembers> LibraryMembers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LibraryMembers>()
                .HasKey(lm => new { lm.LibraryId, lm.MemberId });

            modelBuilder.Entity<LibraryMembers>()
                .HasOne(lm => lm.Library)
                .WithMany(l => l.LibraryMembers)
                .HasForeignKey(lm => lm.LibraryId);

            modelBuilder.Entity<LibraryMembers>()
                .HasOne(lm => lm.Member)
                .WithMany(l => l.LibraryMembers)
                .HasForeignKey(lm => lm.MemberId);

            modelBuilder.Entity<Loan>()
                .HasKey(l => new{l.MemberId, l.BookId});

            modelBuilder.Entity<Loan>()
                .HasOne(lm => lm.Book )
                .WithMany(l => l.Loans)
                .HasForeignKey(lm => lm.BookId);

            modelBuilder.Entity<Loan>()
                .HasOne(lm => lm.Member)
                .WithMany(l => l.Loans)
                .HasForeignKey(lm => lm.MemberId);
        }
    }
}
