using LibraryManagement.Application.DTOs.Library;
using LibraryManagement.Model;
using LibraryManagement.Model.Shows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Interfaces
{
    public interface ILibraryService
    {
        Task<List<LibraryDto>> GetLibrariesAsync();
        Task<List<LibraryNameDto>> GetLibraryNamesAsync();
        Task<LibraryDto?> GetLibraryByIdAsync(Guid id);
        Task<string> CreateLibraryAsync(AddLibraryDto addLibrary);
    }
}
