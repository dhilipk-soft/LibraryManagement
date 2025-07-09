using LibraryManagement.Application.DTOs.Loan;
using LibraryManagement.Model;
using LibraryManagement.Model.Shows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Interfaces
{
    public interface ILoanService
    {
        List<ShowLoanDto> GetAllLoans();
        LoanDto GetLoanById(Guid id);
        string CreateLoan(AddLoanDto dto);
        string ReturnLoan(Guid id);
        string DeleteLoan(Guid id);
    }
}
