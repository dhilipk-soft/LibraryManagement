using AutoMapper;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Model;
using LibraryManagement.Model.Entities;
using LibraryManagement.Model.Shows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;

        public BookRepository(LibraryContext libraryContext) {
            this._context = libraryContext;
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public Book? GetBookById(Guid id)
        {
            return _context.Books.FirstOrDefault(b => b.Id == id);
        }

        public Book AddBook(Book book)
        {
            
            _context.Books.Add(book);
            _context.SaveChanges();

            return book;

        }

        public Book UpdateBook(Book book)
        {

            _context.Books.Update(book);

            _context.SaveChanges();

            return book;
        }

        public Book GetBookByTitleAndLibrary(string Title, Guid id)
        {
            var book =_context.Books.FirstOrDefault(l => l.Title == Title && l.LibraryId == id);

            return book;
        }
    }
}
