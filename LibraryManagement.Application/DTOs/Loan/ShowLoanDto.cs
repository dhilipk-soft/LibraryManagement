

namespace LibraryManagement.Application.DTOs.Loan
{
    public class ShowLoanDto
    {
        public Guid BookId { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public DateTime PublishDate { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }

        public List<LoanMemberDto> Members { get; set; }
    }
}
