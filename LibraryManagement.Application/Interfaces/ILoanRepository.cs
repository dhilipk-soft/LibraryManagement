using LibraryManagement.Application.DTOs.Loan;
using LibraryManagement.Model;
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
        Task<List<LoanDisplayDto>> GetAllLoansAsync();
        Task<ShowLoanDto?> GetLoanByIdAsync(Guid loanId);
        Task<List<LoanDisplayDto>?> GetLoanByMember(Guid id);
        Task<Loan> AddLoanAsync(AddLoanDto loan);
        Task<Loan> DeleteLoanAsync(Guid id);
        Task SaveChangesAsync();
        Task<Book?> GetBookByIdAsync(Guid bookId);
        Task<Member?> GetMemberByIdAsync(Guid memberId);
        Task<Loan?> GetActiveLoanAsync(Guid bookId, Guid memberId);
        Task<Loan> ReturnBookAsync(Guid loanId);
    }
}
