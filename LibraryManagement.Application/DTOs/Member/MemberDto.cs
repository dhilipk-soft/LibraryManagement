namespace LibraryManagement.Model.Shows
{
    public class LoanMemberDto
    {
        public Guid MemberId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
