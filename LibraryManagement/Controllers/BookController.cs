using AutoMapper;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Infrastructure;
using LibraryManagement.Model;
using LibraryManagement.Model.Entities;
using LibraryManagement.Model.Shows;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            this._bookService = bookService;
        }

        // GET: api/Book
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _bookService.GetAllBooks(); // ← await here
            return Ok(result); // ← returns actual data, not Task object
        }


        // GET: api/Book/{id}
        [HttpGet("{id:guid}")]
        public IActionResult GetBookById(Guid id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound("Book not found.");
            }

            return Ok(book);
        }


        // POST: api/Book
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] AddBookDto dto)
        {
            try
            {
                // Step 1: Create the book using your service
                var addedBook = await _bookService.AddBook(dto);

                // Step 2: Return a 201 Created response with the location of the new book
                return CreatedAtAction(
                    nameof(GetBookById),          // Name of the action that retrieves the book
                    new { id = addedBook.Id },    // Route values (in this case, the book's ID)
                    addedBook                     // The book data itself in the response body
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateBookDto dto)
        {
            try
            {
                var updatedBook = await _bookService.UpdateBook(dto, id);
                if (updatedBook == null)
                    return NotFound(new { message = "Book not found" });

                return Ok(updatedBook);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
