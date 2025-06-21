namespace LibraryManagement.Model.Entities
{
    public class Member
    {
        public Guid MemberId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime MemberShipDate {  get; set; }
    }
}
