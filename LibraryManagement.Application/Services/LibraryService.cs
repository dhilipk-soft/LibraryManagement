using LibraryManagement.Application.DTOs.Library;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Model;
using LibraryManagement.Model.Entities;
using LibraryManagement.Model.Shows;

namespace LibraryManagement.Application.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly ILibraryRepository _repository;

        public LibraryService(ILibraryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<LibraryDto>> GetLibrariesAsync()
        {
            var libraries = await _repository.GetLibrariesWithMembersAsync();

            return libraries.Select(l => new LibraryDto
            {
                LibraryId = l.Id,
                LibraryName = l.LibraryName,
                Members = l.LibraryMembers.Select(m => new LoanMemberDto
                {
                    MemberId = m.MemberId,
                    FullName = m.Member.FullName,
                    Email = m.Member.Email,
                    Phone = m.Member.Phone
                }).ToList()
            }).ToList();
        }

        public async Task<List<LibraryNameDto>> GetLibraryNamesAsync()
        {
            var libraries = await _repository.GetAllLibrariesAsync();
            return libraries.Select(l => new LibraryNameDto
            {
                LibraryId = l.Id,
                LibraryName = l.LibraryName
            }).ToList();
        }

        public async Task<LibraryDto?> GetLibraryByIdAsync(Guid id)
        {
            var library = await _repository.GetLibraryByIdAsync(id);
            if (library == null) return null;

            return new LibraryDto
            {
                LibraryId = library.Id,
                LibraryName = library.LibraryName,
                Members = library.LibraryMembers.Select(m => new LoanMemberDto
                {
                    MemberId = m.MemberId,
                    FullName = m.Member.FullName,
                    Email = m.Member.Email,
                    Phone = m.Member.Phone
                }).ToList()
            };
        }

        public async Task<string> CreateLibraryAsync(AddLibraryDto addLibrary)
        {
            var validMembers = await _repository.GetValidMembersAsync(addLibrary.MembersIds);

            var library = new Library
            {
                Id = Guid.NewGuid(),
                LibraryName = addLibrary.LibraryName
            };

            var libraryMembers = validMembers.Select(m => new LibraryMembers
            {
                LibraryId = library.Id,
                MemberId = m.MemberId
            }).ToList();

            await _repository.AddLibraryAsync(library, libraryMembers);

            return "Library and member added successfully";
        }
    }
}
