namespace LibraryManagement.Model
{
    public class AddBookDto
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int TotalCopies { get; set; }
        public Guid CategoryId { get; set; }
        public Guid LibraryId { get; set; }
    }
}
