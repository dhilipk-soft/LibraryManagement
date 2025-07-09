namespace LibraryManagement.Model
{
    public class AddLibraryDto
    {
        public string LibraryName { get; set; }

        public List<Guid> MembersIds { get; set; }
    }
}
