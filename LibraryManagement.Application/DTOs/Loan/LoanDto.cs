namespace LibraryManagement.Model.Shows
{
    public class LoanDto
    {
        public Guid LoanId { get; set; }
        public Guid MemberId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueTime { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
