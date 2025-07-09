namespace LibraryManagement.Model.Shows
{
    public class BookWithMemberDto
    {
        public Guid BookId { get; set; }

        public required string Title { get; set; }

        public required string Author { get; set; }

        public required DateTime PublishDate { get; set; }

        public required int TotalCopies { get; set; }

        public required int AvailableCopies { get; set; }

        public List<LoanMemberDto> members { get; set; }
    }
}
