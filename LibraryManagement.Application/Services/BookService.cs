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

namespace LibraryManagement.Application.Services
{
    public class BookService : IBookService
    {

        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public BookService(
            IBookRepository bookRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper
            ) {
            this._mapper = mapper;
            this._bookRepository= bookRepository;
            this._categoryRepository = categoryRepository;
        }

        ShowBookDto IBookService.AddBook(AddBookDto dto)
        {
            var category = _categoryRepository.GetCategoryById(dto.CategoryId);
            if(category == null)
            {
                throw new ArgumentException("Category not found");
            }
 

            var newBook = _mapper.Map<Book>(dto);
            newBook.Id = Guid.NewGuid();
            newBook.PublishDate = DateTime.Now;

            _bookRepository.AddBook(newBook);

            return _mapper.Map<ShowBookDto>(newBook);
        }

        List<ShowBookDto> IBookService.GetAllBooks()
        {
            return _bookRepository.GetAllBooks()
                .Select(book => _mapper.Map<ShowBookDto>(book))
                .ToList();  
        }

        ShowBookDto? IBookService.GetBookById(Guid id)
        {
            return _mapper.Map<ShowBookDto>(_bookRepository.GetBookById(id));
        }

        ShowBookDto IBookService.UpdateBook(UpdateBookDto dto, Guid id)
        {
            var existingBook = _bookRepository.GetBookById(id); 
            if(existingBook == null)
            {
                throw new ArgumentException("Book not found");
            }

            _mapper.Map(dto, existingBook);

            var updatedBook = _bookRepository.UpdateBook(existingBook);

            return _mapper.Map<ShowBookDto>(updatedBook);
        }
    }
}
