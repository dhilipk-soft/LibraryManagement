namespace LibraryManagement.Model.Shows
{
    public class LibraryDto
    {
        public Guid LibraryId { get; set; }
        public string LibraryName { get; set; }
        public List<LoanMemberDto> Members { get; set; }
    }
}
