using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Model.Entities
{
    public class Book
    {
        public Guid Id { get; set; }

        public required string Title { get; set; }

        public required string Author { get; set; }

        public required DateTime PublishDate { get; set; }

        public required int TotalCopies { get; set; }

        public required int AvailableCopies { get; set; }

        public Guid CategoryId { get; set; }

        [ForeignKey("Library")]
        public Guid LibraryId { get; set; }

        public Library Library { get; set; }

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
 
    }
}
