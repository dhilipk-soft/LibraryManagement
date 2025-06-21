namespace LibraryManagement.Model.Entities
{
    public class Librarian
    {
        public Guid LibrarianId { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }
    }
}
