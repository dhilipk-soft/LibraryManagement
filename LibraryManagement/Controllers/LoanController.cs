using LibraryManagement.Data;
using LibraryManagement.Model;
using LibraryManagement.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly LibraryContext dbContext;

        public LoanController(LibraryContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/Loan
        [HttpGet]
        public IActionResult GetAllLoans()
        {
            var loans = dbContext.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .ToList();

            return Ok(loans);
        }

        // GET: api/Loan/{id}
        [HttpGet("{id:guid}")]
        public IActionResult GetLoanById(Guid id)
        {
            var loan = dbContext.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .FirstOrDefault(l => l.LoanId == id);

            if (loan == null)
            {
                return NotFound("Loan not found.");
            }

            return Ok(loan);
        }

        // POST: api/Loan
        [HttpPost]
        public IActionResult CreateLoan(AddLoanDto dto)
        {
            // Check if Book exists
            var book = dbContext.Books.FirstOrDefault(b => b.Id == dto.BookId);
            if (book == null)
                return BadRequest("Book not found.");

            // Check if Member exists
            var member = dbContext.Members.FirstOrDefault(m => m.MemberId == dto.MemberId);
            if (member == null)
                return BadRequest("Member not found.");

            // Prevent duplicate active loan for same book
            var activeLoan = dbContext.Loans.FirstOrDefault(l =>
                l.BookId == dto.BookId &&
                l.ReturnDate == null);

            if (activeLoan != null)
                return BadRequest("This book is already loaned out and not returned yet.");

            var loan = new Loan
            {
                LoanId = Guid.NewGuid(),
                BookId = dto.BookId,
                MemberId = dto.MemberId,
                IssueDate = DateTime.UtcNow,
                DueTime = dto.DueTime,
                ReturnDate = default // Not returned yet
            };

            dbContext.Loans.Add(loan);
            dbContext.SaveChanges();

            // Reduce available copies
            book.AvailableCopies--;
            dbContext.SaveChanges();

            return Ok(loan);
        }

        // PUT: api/Loan/return/{id}
        [HttpPut("return/{id:guid}")]
        public IActionResult ReturnLoan(Guid id)
        {
            var loan = dbContext.Loans.FirstOrDefault(l => l.LoanId == id);
            if (loan == null)
                return NotFound("Loan not found.");

            if (loan.ReturnDate != default)
                return BadRequest("Loan already returned.");

            loan.ReturnDate = DateTime.UtcNow;

            // Increase available copies
            var book = dbContext.Books.FirstOrDefault(b => b.Id == loan.BookId);
            if (book != null)
                book.AvailableCopies++;

            dbContext.SaveChanges();

            return Ok("Book returned successfully.");
        }
    }
}
