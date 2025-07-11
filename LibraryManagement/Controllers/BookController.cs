using AutoMapper;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Infrastructure;
using LibraryManagement.Model;
using LibraryManagement.Model.Entities;
using LibraryManagement.Model.Shows;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            this._bookService = bookService;
        }

        // GET: api/Book
        [HttpGet]
        public IActionResult GetAllBooks()
        {

            //_bookService.GetAllBooks();

            return Ok(_bookService.GetAllBooks());
        }

        // GET: api/Book/{id}
        [HttpGet("{id:guid}")]
        public IActionResult GetBookById(Guid id)
        {
            var book = _bookService.GetBookById(id);
            if(book == null)
            {
                return NotFound("Book not found.");
            }

            return Ok(book);
        }

        // POST: api/Book
        [HttpPost]
        public IActionResult AddBook(AddBookDto dto)
        {
            try
            {
                var addedBook = _bookService.AddBook(dto);
                return CreatedAtAction(nameof(GetBookById), new { id = addedBook.Id }, addedBook);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]

        public IActionResult UpdateBook([FromBody] UpdateBookDto updateBook, Guid id)
        {

            //console

            try
            {
                var updatedBook = _bookService.UpdateBook(updateBook, id);
                return Ok(updatedBook);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
