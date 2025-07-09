using LibraryManagement.Application.DTOs.Loan;
using LibraryManagement.Application.Interfaces;
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
        private readonly ILoanService loanService;

        public LoanController(LibraryContext dbContext, ILoanService loanService)
        {
            this.dbContext = dbContext;
            this.loanService = loanService;
        }

        // GET: api/Loan
        [HttpGet]
        public IActionResult GetAllLoans()
        {
            try
            {
                var loan = loanService.GetAllLoans();
                if (loan == null || !loan.Any())
                {
                    return NotFound("No loans found.");
                }
                return Ok(loan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Loan/{id}
        [HttpGet("{id:guid}")]
        public IActionResult GetLoanById(Guid id)
        {
            try
            {
                var loan = loanService.GetLoanById(id);
                if (loan == null)
                {
                    return NotFound("Loan not found.");
                }
                return Ok(loan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Loan
        [HttpPost]
        public IActionResult CreateLoan(AddLoanDto dto)
        {
            try
            {
                var loan = loanService.CreateLoan(dto);
                
                return Ok(loan);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // 404
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message }); // 400
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" }); // 500
            }
        }

        // PUT: api/Loan/return/{id}
        [HttpPut("{id:guid}")]
        public IActionResult ReturnLoan(Guid id)
        {
            try
            {
                var loan = loanService.ReturnLoan(id);

                return Ok(new { message = "Book returned successfully." });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }


        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteLoan(Guid id)
        {
            try
            {
                var loan = loanService.DeleteLoan(id);
                if (loan == null)
                {
                    return NotFound("Loan not found.");
                }
                return Ok(loan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
