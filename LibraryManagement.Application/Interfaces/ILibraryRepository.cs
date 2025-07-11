using LibraryManagement.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Interfaces
{
    public interface ILibraryRepository
    {
        Task<List<Library>> GetAllLibrariesAsync();
        Task<List<Library>> GetLibrariesWithMembersAsync();
        Task<Library?> GetLibraryByIdAsync(Guid id);
        Task<List<Member>> GetValidMembersAsync(List<Guid> memberIds);
        Task AddLibraryAsync(Library library, List<LibraryMembers> members);
    }
}
