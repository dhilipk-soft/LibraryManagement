using LibraryManagement.Application.DTOs.Loan;
using LibraryManagement.Model;
using LibraryManagement.Model.Entities;
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
        List<LoanDisplayDto> GetAllLoans();
        ShowLoanDto GetLoanById(Guid id);
        LoanDto CreateLoan(AddLoanDto dto);
        ShowLoanDto ReturnLoan(Guid id);
        ShowLoanDto DeleteLoan(Guid id);
    }
}
