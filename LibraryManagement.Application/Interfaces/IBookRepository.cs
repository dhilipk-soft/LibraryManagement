using LibraryManagement.Model.Entities;

namespace LibraryManagement.Application.Interfaces
{
    public interface IBookRepository
    {
        public List<Book> GetAllBooks();
        public Book? GetBookById(Guid id);
        public Book AddBook(Book book);
        public Book UpdateBook(Book book);

        public Book GetBookByTitleAndLibrary(string Title, Guid id);
    }
}
