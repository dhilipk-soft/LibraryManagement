using LibraryManagement.Model.Entities;

namespace LibraryManagement.Application.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooks();
        Task<Book?> GetBookById(Guid id);
        Task<Book> AddBook(Book book);
        Task<Book> UpdateBook(Book book);
        Task<Book?> GetBookByTitleAndLibrary(string title, Guid libraryId);
    }
}
