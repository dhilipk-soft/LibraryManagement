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
        List<ShowBookDto> GetAllBooks();
        ShowBookDto? GetBookById(Guid id);
        ShowBookDto AddBook(AddBookDto dto);
        ShowBookDto UpdateBook(UpdateBookDto dto, Guid id);
    }
}
