namespace LibraryManagement.Model.Entities
{
    public class LibraryMembers
    {
        public Guid LibraryId { get; set; }
        public Library Library { get; set; }

        public Guid MemberId { get; set; }
        public Member Member { get; set; }

        public DateOnly JoinedOn { get; set; }

    }
}
