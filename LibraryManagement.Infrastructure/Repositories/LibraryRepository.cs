using LibraryManagement.Application.Interfaces;
using LibraryManagement.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly LibraryContext _context;

        public LibraryRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<List<Library>> GetLibrariesWithMembersAsync()
        {
            return await _context.Libraries
                .Include(l => l.LibraryMembers)
                    .ThenInclude(lm => lm.Member)
                .ToListAsync();
        }

        public async Task<List<Library>> GetAllLibrariesAsync()
        {
            return await _context.Libraries.ToListAsync();
        }

        public async Task<Library?> GetLibraryByIdAsync(Guid id)
        {
            return await _context.Libraries
                .Where(l => l.Id == id)
                .Include(l => l.LibraryMembers)
                    .ThenInclude(lm => lm.Member)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Member>> GetValidMembersAsync(List<Guid> memberIds)
        {
            return await _context.Members
                .Where(m => memberIds.Contains(m.MemberId))
                .ToListAsync();
        }

        public async Task AddLibraryAsync(Library library, List<LibraryMembers> members)
        {
            _context.Libraries.Add(library);
            _context.LibraryMembers.AddRange(members);
            await _context.SaveChangesAsync();
        }
    }
}
