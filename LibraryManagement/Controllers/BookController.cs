using LibraryManagement.Data;
using LibraryManagement.Model;
using LibraryManagement.Model.Entities;
using LibraryManagement.Model.Shows;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly LibraryContext dbContext;

        public BookController(LibraryContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/Book
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = dbContext.Books
                .Include(b => b.Category)
                .Select(b => new ShowBookDto
                {
                    Title = b.Title,
                    Author = b.Author,
                    TotalCopies = b.TotalCopies,
                    AvailableCopies = b.AvailableCopies,
                    CategoryName = b.Category.CategoryName,
                    PublishDate = b.PublishDate.ToString(),
                    Id = b.CategoryId
                })
                .ToList();

            return Ok(books);
        }

        // GET: api/Book/{id}
        [HttpGet("{id:guid}")]
        public IActionResult GetBookById(Guid id)
        {
            var book = dbContext.Books
                .Include(b => b.Category)
                .Where(b => b.Id == id)
                .Select(b => new ShowBookDto
                {
                    Title = b.Title,
                    Author = b.Author,
                    TotalCopies = b.TotalCopies,
                    AvailableCopies = b.AvailableCopies,
                    CategoryName = b.Category.CategoryName

                })
                .FirstOrDefault();

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // POST: api/Book
        [HttpPost]
        public IActionResult AddBook(AddBookDto dto)
        {
            // Validate if Category exists
            var category = dbContext.Categoryies.Find(dto.CategoryId);
            if (category == null)
            {
                return BadRequest("Invalid category.");
            }
            var existingBook = dbContext.Books
            .FirstOrDefault(b => b.Title.ToLower() == dto.Title.ToLower());

            if (existingBook != null)
            {
                return Conflict("A book with the same title already exists.");
            }

            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Author = dto.Author,
                TotalCopies = dto.TotalCopies,
                AvailableCopies = dto.AvailableCopies,
                CategoryId = dto.CategoryId,
                PublishDate = DateTime.Now
            };



            dbContext.Books.Add(book);
            dbContext.SaveChanges();

            return Ok(book);
        }
    }
}
