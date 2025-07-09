using LibraryManagement.Model.Entities;

namespace LibraryManagement.Model.Shows
{
    public class ShowLibraryMember
    {
        public Guid MemberId { get; set; }
        public Member Member { get; set; }

    }
}
