namespace LibraryManagement.Model.Shows
{
    public class ShowBookDto
    {
        public Guid Id { get; set; }            // Unique identifier of the book
        public string? Title { get; set; }       // Title of the book
        public string? Author { get; set; }      // Author name
        public int TotalCopies { get; set; }    // Total number of copies
        public int AvailableCopies { get; set; }    // ID of the category
        public Guid CategoryId { get; set; } // Optional: Name of the category
        public string? PublishDate { get; set; }
    }

}
