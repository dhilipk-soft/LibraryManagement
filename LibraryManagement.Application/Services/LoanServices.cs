using AutoMapper;
using LibraryManagement.Application.DTOs.Loan;
using LibraryManagement.Application.Interfaces;
using LibraryManagement.Model;
using LibraryManagement.Model.Entities;
using LibraryManagement.Model.Shows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Services
{
    public class LoanServices : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;
        public LoanServices(
            ILoanRepository loanRepository,
            IMapper mapper
            ) {
            this._loanRepository = loanRepository;
            this._mapper = mapper;
        }
        LoanDto ILoanService.CreateLoan(AddLoanDto dto)
        {
            var loan = _loanRepository.AddLoanAsync(dto)
                .GetAwaiter()
                .GetResult();

            return _mapper.Map<LoanDto>(loan);

        }

        ShowLoanDto ILoanService.DeleteLoan(Guid id)
        {
            var loan = _loanRepository.DeleteLoanAsync(id).GetAwaiter().GetResult();
            return _mapper.Map<ShowLoanDto>(loan);
        }

        List<LoanDisplayDto> ILoanService.GetAllLoans()
        {
            return _loanRepository.GetAllLoansAsync().GetAwaiter().GetResult();
        }

        ShowLoanDto ILoanService.GetLoanById(Guid id)
        {
            var loan = _loanRepository.GetLoanByIdAsync(id).GetAwaiter().GetResult();
            return _mapper.Map<ShowLoanDto>(loan);
        }

        ShowLoanDto ILoanService.ReturnLoan(Guid id)
        {
            var loan = _loanRepository.ReturnBookAsync(id).GetAwaiter().GetResult();
            return _mapper.Map<ShowLoanDto>(loan);
        }
    }
}
