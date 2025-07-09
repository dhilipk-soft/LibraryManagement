namespace LibraryManagement.Model.Shows
{
    public class UpdateBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
        public Guid CategoryId { get; set; }
    }
}
