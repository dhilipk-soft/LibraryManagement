using LibraryManagement.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Interfaces
{
    public interface ILoanRepository
    {
        Task<List<Loan>> GetAllLoansAsync();
        Task<Loan?> GetLoanByIdAsync(Guid loanId);
        Task AddLoanAsync(Loan loan);
        Task DeleteLoanAsync(Loan loan);
        Task SaveChangesAsync();
        Task<Book?> GetBookByIdAsync(Guid bookId);
        Task<Member?> GetMemberByIdAsync(Guid memberId);
        Task<Loan?> GetActiveLoanAsync(Guid bookId, Guid memberId);
    }
}
