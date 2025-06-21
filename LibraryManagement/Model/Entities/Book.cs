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

        public Category Category { get; set; }
 
    }
}
