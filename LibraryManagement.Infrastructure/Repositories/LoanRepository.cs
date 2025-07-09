using LibraryManagement.Application.Interfaces;
using LibraryManagement.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LibraryContext _context;

        public LoanRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<List<Loan>> GetAllLoansAsync()
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .ToListAsync();
        }

        public async Task<Loan?> GetLoanByIdAsync(Guid loanId)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Member)
                .FirstOrDefaultAsync(l => l.LoanId == loanId);
        }

        public async Task AddLoanAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
        }

        public async Task DeleteLoanAsync(Loan loan)
        {
            _context.Loans.Remove(loan);
            await Task.CompletedTask; // Just to satisfy method signature
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
