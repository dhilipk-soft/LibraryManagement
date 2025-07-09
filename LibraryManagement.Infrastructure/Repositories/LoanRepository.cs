using LibraryManagement.Application.DTOs.Loan;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Model;
using LibraryManagement.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Web.Http.Results;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LibraryContext _context;

        public LoanRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Loan> ReturnBookAsync(Guid loanId)
        {
            var loan = await _context.Loans
                .FirstOrDefaultAsync(l => l.LoanId == loanId);
            if (loan == null)
                throw new KeyNotFoundException("Loan not found.");

            loan.ReturnDate = DateTime.UtcNow;
            var book = await _context.Books
                .FirstOrDefaultAsync(b => b.Id == loan.BookId);
            if (book == null)
                throw new KeyNotFoundException("Book not found.");
            book.AvailableCopies++;
            _context.Loans.Update(loan);
            _context.Books.Update(book);
            _context.SaveChanges();

            return loan;
        }

        public async Task<List<LoanDisplayDto>> GetAllLoansAsync()
        {
            return await _context.Loans
                    .Include(l => l.Book)
                    .Include(l => l.Member)
                    .Select(l => new LoanDisplayDto
                    {
                        LoanId = l.LoanId,
                        BookId = l.Book.Id,
                        Title = l.Book.Title,
                        Author = l.Book.Author,
                        MemberId = l.Member.MemberId,
                        FullName = l.Member.FullName,
                        Email = l.Member.Email,
                        Phone = l.Member.Phone,
                        IssueDate = l.IssueDate,
                        DueTime = l.DueTime,
                        ReturnDate = l.ReturnDate
                    })
                    .ToListAsync(); // Convert the whole query to a list
                    }


        public async Task<ShowLoanDto?> GetLoanByIdAsync(Guid loanId)
        {
            var find = await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .FirstOrDefaultAsync(l => l.LoanId == loanId);

            return find.LoanId != Guid.Empty ? new ShowLoanDto
            {
                BookId = find.Book.Id,
                Title = find.Book.Title,
                Author = find.Book.Author,
                PublishDate = find.Book.PublishDate,
                TotalCopies = find.Book.TotalCopies,
                AvailableCopies = find.Book.AvailableCopies,
                Members = new List<LoanMemberDto>
                {
                    new LoanMemberDto
                    {
                        LoanId = find.LoanId,
                        MemberId = find.Member.MemberId,
                        FullName = find.Member.FullName,
                        Email = find.Member.Email,
                        Phone = find.Member.Phone,
                        IssueDate = find.IssueDate,
                        DueTime = find.DueTime,
                        ReturnDate = find.ReturnDate
                    }
                }
            } : null;
        }

        public async Task<Loan> AddLoanAsync(AddLoanDto dto)
        {
            var book = await GetBookByIdAsync(dto.BookId);
            if (book == null)
                throw new KeyNotFoundException("Book not found.");
            var member = await GetMemberByIdAsync(dto.MemberId);
            if (member == null)
                throw new KeyNotFoundException("Member not found.");

            if (book.AvailableCopies <= 0)
                throw new InvalidOperationException("No available copies of the book.");
            var activeLoan = await GetActiveLoanAsync(dto.BookId, dto.MemberId);
            if (activeLoan != null && activeLoan.ReturnDate == null)
                throw new InvalidOperationException("This book is already loaned out and not returned yet.");
            var loan = new Loan
            {
                LoanId = Guid.NewGuid(),
                BookId = dto.BookId,
                MemberId = dto.MemberId,
                IssueDate = DateTime.UtcNow
                    .AddTicks(-(DateTime.UtcNow.Ticks % TimeSpan.TicksPerSecond)),
                DueTime = DateTime.UtcNow.AddDays(14),
                ReturnDate = null
            };

            await _context.Loans.AddAsync(loan);

            book.AvailableCopies--;
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task<Loan> DeleteLoanAsync(Guid id)
        {
            var loan = await _context.Loans
                .FirstOrDefaultAsync(l => l.LoanId == id);
            if(loan == null)
                throw new KeyNotFoundException("Loan not found.");
            
            var book = await _context.Books
                .FirstOrDefaultAsync(b => b.Id == loan.BookId);
            if (book == null)
                throw new KeyNotFoundException("Book not found.");

            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
            book.AvailableCopies++;
            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return loan;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Book?> GetBookByIdAsync(Guid bookId)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId);
        }

        public async Task<Member?> GetMemberByIdAsync(Guid memberId)
        {
            return await _context.Members.FirstOrDefaultAsync(m => m.MemberId == memberId);
        }

        public async Task<Loan?> GetActiveLoanAsync(Guid bookId, Guid memberId)
        {
            return await _context.Loans
                .FirstOrDefaultAsync(l => l.BookId == bookId && l.MemberId == memberId && l.ReturnDate == null);
        }
    }
}
