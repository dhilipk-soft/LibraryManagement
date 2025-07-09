    namespace LibraryManagement.Model.Entities
    {
        public class Member
        {
            public Guid MemberId { get; set; }

            public string? FullName { get; set; }

            public string? Email { get; set; }

            public string? Phone { get; set; }

            public ICollection<LibraryMembers> LibraryMembers { get; set; } = new List<LibraryMembers>();

            public ICollection<Loan> Loans { get; set; } = new List<Loan>();

        }
    }
