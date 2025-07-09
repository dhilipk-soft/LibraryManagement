using LibraryManagement.Application.DTOs.Loan;
using LibraryManagement.Infrastructure;
using LibraryManagement.Model;
using LibraryManagement.Model.Entities;
using LibraryManagement.Model.Shows;
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
            var loans = dbContext.Books
                     .Include(l => l.Loans)
                    .ThenInclude(l => l.Member)
                 .Select(group => new ShowLoanDto
                 {
                     BookId = group.Id,
                     Title = group.Title,
                     Author = group.Author,
                     PublishDate = group.PublishDate,
                     TotalCopies = group.TotalCopies,
                     AvailableCopies = group.AvailableCopies,
                     Members = group.Loans.Select(l => new Application.DTOs.Loan.LoanMemberDto
                     {
                         LoanId = l.LoanId,
                         MemberId = l.Member.MemberId,
                         FullName = l.Member.FullName,
                         Email = l.Member.Email,
                         Phone = l.Member.Phone,
                         IssueDate = l.IssueDate,
                         DueTime = l.DueTime,
                         ReturnDate = l.ReturnDate

                     }).ToList()
                 })
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
                 l.MemberId == dto.MemberId);

            if (activeLoan != null)
                return BadRequest("This book is already loaned out and not returned yet.");

            var addLoan = new Loan
            {
                LoanId = Guid.NewGuid(),
                BookId = dto.BookId,
                MemberId = dto.MemberId,
                IssueDate = DateTime.UtcNow
                            .AddTicks(-(DateTime.UtcNow.Ticks % TimeSpan.TicksPerSecond)),
                DueTime = DateTime.UtcNow.AddDays(14),
                ReturnDate = null
            };

            dbContext.Loans.Add(addLoan);
            dbContext.SaveChanges();

            // Reduce available copies
            book.AvailableCopies--;
            dbContext.SaveChanges();


            return Ok(dto);
        }

        // PUT: api/Loan/return/{id}
        [HttpPut("{id:guid}")]
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

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteLoan(Guid id)
        {
            var loan = dbContext.Loans.FirstOrDefault(l => l.LoanId == id);
            if (loan == null)
                return NotFound("Loan not found.");
            var book = dbContext.Books.FirstOrDefault(l => l.Id == loan.BookId);
            if (book == null)
                return NotFound();
            
            book.AvailableCopies++;
            dbContext.Loans.Remove(loan);
            dbContext.SaveChanges();
            return Ok(new { message = "Loan deleted successfully." });
        }
    }
}
