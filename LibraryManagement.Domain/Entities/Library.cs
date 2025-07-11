using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Model.Entities
{
    public class Library
    {
        public Guid Id { get; set; }

        public required string LibraryName { get; set; }

        public ICollection<LibraryMembers> LibraryMembers { get; set; } = new List<LibraryMembers>();

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
