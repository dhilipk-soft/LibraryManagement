using LibraryManagement.Model;
using LibraryManagement.Model.Shows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Interfaces
{
    public interface IBookService
    {
        Task<ShowBookDto> AddBook(AddBookDto dto);
        Task<List<ShowBookDto>> GetAllBooks(); // ⬅️ Changed to async
        Task<ShowBookDto?> GetBookById(Guid id); // ⬅️ Also made nullable return async
        Task<ShowBookDto> UpdateBook(UpdateBookDto dto, Guid id);
    }




}
